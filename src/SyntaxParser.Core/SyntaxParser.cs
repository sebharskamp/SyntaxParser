using SyntaxParser.Core;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SyntaxParser
{
    /// <summary>
    /// 
    /// </summary>
    public static class SyntaxParser
    {
        private static readonly ConcurrentDictionary<Type, (Delegate del, string[] delimeters)> _parsers = new();
        private static readonly int _sequenceAlgorithm = SequenceAlgorithm.Naive;

        /// <summary>
        /// Parse a full text, splitted into lines by Environement.Newline.
        /// </summary>
        /// <param name="text">The full text.</param>
        /// <returns>Parsed instances of T.</returns>
        public static IEnumerable<T> ParseText<T>(string text) where T : notnull
        {
            return GetParser<T>()(text);
        }

        /// <summary>
        /// Parse text, splitted into lines by Environement.Newline, to serialized JSON object(s).
        /// </summary>
        /// <param name="text">The full text.</param>
        /// <returns>serialized JSON object.</returns>
        public static string ParseTextToJson<T>(string text) where T : notnull
        {
            return JsonSerializer.Serialize(SyntaxParser.ParseText<T>(text));
        }

        /// <summary>
        /// Parse content of a file line by line to create the desired instances.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>Parsed instances.</returns>
        public static IEnumerable<T> ParseFile<T>(string path) where T : notnull
        {
            using var sr = new StreamReader(path);
            return GetParser<T>()(sr.ReadToEnd());
        }

        /// <summary>
        /// Parse content of a file line by line to create the desired instances asyncronous.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>Parsed instances.</returns>
        public static async IAsyncEnumerable<T> ParseFileAsync<T>(string path) where T : notnull
        {
            using var sr = new StreamReader(path); ;
            while (sr.Peek() >= 0)
            {
                var line = await sr.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    foreach (var result in GetParser<T>()(line))
                    {
                        yield return result;
                    }
                }
            }
        }

        /// <summary>
        /// Parse content of a file line by line to serialized JSON object(s).
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>serialized JSON object.</returns>
        public static string ParseFileToJson<T>(string path) where T : notnull
        {
            return JsonSerializer.Serialize(SyntaxParser.ParseFile<T>(path));
        }

        /// <summary>
        /// Parse content of a file line by line to serialized JSON object(s).
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>serialized JSON object.</returns>
        public async static Task<string> ParseFileToJsonAsync<T>(string path) where T : notnull
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, SyntaxParser.ParseFileAsync<T>(path));
            using var reader = new StreamReader(stream);
            stream.Position = 0;
            return reader.ReadToEnd();
        }

        private static Func<string, T[]> GetParser<T>() where T : notnull
        {
            var key = typeof(T);
            if (!_parsers.TryGetValue(key, out var value))
                value = AddToHash(_parsers, key, type => CreateParser(type));
            return input =>
            {
                return ((Func<string[], T[]>)value.del)(input.ToStructuredArray<string>(value.delimeters, 0, 0, _sequenceAlgorithm));
            };
        }

        private static (Delegate del, string[] regex) AddToHash(ConcurrentDictionary<Type, (Delegate, string[])> hash, Type key, Func<Type, (Delegate, string[])> func)
        {
            return hash.GetOrAdd(key, type =>
            {
                var del = func(type);
                hash[key] = del;
                return del;
            });
        }

        private static (Delegate, string[]) CreateParser(Type type)
        {
            string[]? syntaxDelimiter = null;

            var attributes = type.GetCustomAttributes().ToArray();
            var syntax = ((SyntaxAttribute?)attributes.FirstOrDefault(a => a.GetType() == typeof(SyntaxAttribute)));
            var delimiterSyntax = ((SingleDelimiterSyntaxAttribute?)attributes.FirstOrDefault(a => a.GetType() == typeof(SingleDelimiterSyntaxAttribute)));
            string? value = null;
            if (syntax?.Value is not null)
            {
                syntaxDelimiter = ConstantRegex.SyntaxDelimiter.Matches(syntax.Value).Select(m => m.Value).ToArray();
                value = syntax?.Value;
            }
            else if (delimiterSyntax is not null)
            {
                syntaxDelimiter = Enumerable.Repeat(delimiterSyntax.Value, type.GetProperties().Length - 1).ToArray();
                value = string.Join(delimiterSyntax.Value, type.GetProperties().Select(p => p.Name));
            }
            else
            {
                syntaxDelimiter = new string[0];
                value = type.GetProperties().FirstOrDefault(p => p.PropertyType == typeof(string))?.Name ?? throw new InvalidOperationException();
            }

            var activator = BuildCreateInstanceFunction(type, value.ToStructuredArray<string>(syntaxDelimiter, 0, 0, _sequenceAlgorithm)).Compile();
            return (activator, syntaxDelimiter);
        }

        private static LambdaExpression BuildCreateInstanceFunction(Type type, string[] syntax)
        {
            var input = Expression.Parameter(typeof(string[]), "input");
            var index = Expression.Variable(typeof(int), "index");
            var valueAtIndex = Expression.Variable(typeof(string), "valueAtIndex");

            var properties = type.GetProperties().Select(p => p.Name);
            var sliceLength = syntax.Length;
            var orderproperties = new string[sliceLength];
            var shift = Expression.Variable(typeof(int), "shift");

            var propertyAssignments = new MemberAssignment[sliceLength];

            for (var i = 0; i < sliceLength; i++)
            {
                var member = type.GetProperty(syntax[i]);
                if (member is null) throw new InvalidOperationException($"Member {syntax[i]} not found on {type.FullName}");
                IndexExpression memberValueAsString = Expression.ArrayAccess(input, Expression.Add(shift, Expression.Constant(i)));
                if (member.PropertyType == typeof(string))
                {
                    propertyAssignments[i] = Expression.Bind(member, memberValueAsString);
                }
                else if (member.PropertyType.IsArray)
                {
                    var arrayElementType = member.PropertyType.GetElementType();
                    if (arrayElementType is null) throw new InvalidOperationException($"elementType of array for member {member.Name} not defined. Member is on {type.FullName}");
                    var parse = Expression.Call(typeof(StringExtensions), nameof(StringExtensions.ToStructuredArray), new[] { arrayElementType }, new Expression[] { memberValueAsString, Expression.NewArrayInit(typeof(string), new[] { Expression.Constant(",") }), Expression.Constant(1), Expression.Constant(1), Expression.Constant(0) });
                    propertyAssignments[i] = Expression.Bind(member, parse);
                }
                else if (member.PropertyType.IsValueType)
                {
                    var parse = Expression.Call(member.PropertyType, "Parse", null, memberValueAsString);
                    propertyAssignments[i] = Expression.Bind(member, parse);
                }
            }

            var initialization = Expression.Lambda(Expression.MemberInit(Expression.New(type), propertyAssignments), new[] { input, shift });

            var sliceSize = Expression.Variable(typeof(int), "sliceSize");
            var resultSize = Expression.Variable(typeof(int), "resultSize");
            var results = Expression.Variable(type.MakeArrayType(), "results");
            var j = Expression.Variable(typeof(int), "j");
            var @break = Expression.Label("break");

            var assignLoop = Expression.Loop(
                                    Expression.IfThenElse(
                                        Expression.LessThan(j, resultSize),
                                        Expression.Block(
                                            Expression.Assign(shift, Expression.Multiply(j, sliceSize)),
                                            Expression.Assign(Expression.ArrayAccess(results, j), Expression.MemberInit(Expression.New(type), propertyAssignments)),
                                            Expression.PostIncrementAssign(j)),
                                        Expression.Break(@break)),
                                    @break);

            var body = Expression.Block
            (
                new[] { sliceSize, resultSize, results, j, shift },
                Expression.Assign(j, Expression.Constant(0)),
                Expression.Assign(sliceSize, Expression.Constant(sliceLength)),
                Expression.Assign(resultSize, Expression.Divide(Expression.ArrayLength(input), sliceSize)),
                Expression.Assign(results, Expression.NewArrayBounds(type, resultSize)),
                assignLoop,
                results
            );
            return Expression.Lambda(body, new[] { input });
        }
    }
}