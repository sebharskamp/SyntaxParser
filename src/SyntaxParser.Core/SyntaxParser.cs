using SyntaxParser.Core;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SyntaxParser
{
    /// <summary>
    /// 
    /// </summary>
    public static class SyntaxParser
    {
        private static readonly ConcurrentDictionary<Type, (Delegate del, string[] delimeters, SequenceOptions sequenceOptions)> _parsers = new();

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
            return JsonSerializer.Serialize(ParseText<T>(text));
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
        /// Parse content of a file line by line to create the desired instances.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="skip">The amount of lines to skip.</param>
        /// <returns>Parsed instances.</returns>
        public static IEnumerable<T> ParseFile<T>(string path, int skip) where T : notnull
        {
            using var sr = new StreamReader(path);
            int lineNumber = -1;
            while (sr.Peek() >= 0)
            {
                lineNumber++;
                
                if (lineNumber < skip)
                { 
                    sr.ReadLine();
                    continue;
                }
                
                var line = sr.ReadLine();
        
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
        /// Parse content of a file line by line to create the desired instances.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="skip">The amount of lines to skip.</param>
        /// <param name="take">The amount of lines to parse.</param>
        /// <returns>Parsed instances.</returns>
        public static IEnumerable<T> ParseFile<T>(string path, int skip, int take) where T : notnull
        {
            using var sr = new StreamReader(path);
            var toLineNumber = skip + take;
            var lineNumber = -1;
            while (sr.Peek() >= 0)
            {
                lineNumber++;
        
                if (lineNumber == toLineNumber)
                {
                    break;
                }
                
                if (lineNumber < skip)
                { 
                    sr.ReadLine();
                    continue;
                }

                var line = sr.ReadLine();
                
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
        /// Parse content of a file line by line to create the desired instances asyncronous.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="skip">The amount of lines to skip.</param>
        /// <returns>Parsed instances.</returns>
        public static async IAsyncEnumerable<T> ParseFileAsync<T>(string path, int skip) where T : notnull
        {
            using var sr = new StreamReader(path); ;
            int lineNumber = -1;
            while (sr.Peek() >= 0)
            {
                lineNumber++;
                
                if(lineNumber < skip)
                { 
                    await sr.ReadLineAsync();
                    continue;
                }
                
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
        /// Parse content of a file line by line to create the desired instances asyncronous.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="skip">The amount of lines to skip.</param>
        /// <param name="take">The amount of lines to parse.</param>
        /// <returns>Parsed instances.</returns>
        public static async IAsyncEnumerable<T> ParseFileAsync<T>(string path, int skip, int take) where T : notnull
        {
            using var sr = new StreamReader(path); ;
            var toLineNumber = skip + take;
            var lineNumber = -1;
            while (sr.Peek() >= 0)
            {
                lineNumber++;
                
                if(lineNumber == toLineNumber)
                {
                    break;
                }
                
                if(lineNumber < skip)
                { 
                    await sr.ReadLineAsync();
                    continue;
                }
                
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
        /// <param name="skip">Lines to skip.</param>
        /// <param name="take">Lines to take.</param>
        /// <returns>serialized JSON object.</returns>
        public static string ParseFileToJson<T>(string path, int? skip = null, int? take = null) where T : notnull
        {
          return skip.HasValue switch
            {
                true when take.HasValue => JsonSerializer.Serialize(ParseFile<T>(path, skip.Value, take.Value)),
                true => JsonSerializer.Serialize(ParseFile<T>(path, skip.Value)),
                _ => JsonSerializer.Serialize(ParseFile<T>(path))
            };
        }

        /// <summary>
        /// Parse content of a file line by line to serialized JSON object(s).
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="skip">Lines to skip.</param>
        /// <param name="take">Lines to take.</param>
        /// <returns>serialized JSON object.</returns>
        public static async Task<string> ParseFileToJsonAsync<T>(string path, int? skip = null, int? take = null) where T : notnull
        {
            using var stream = new MemoryStream();

            if (skip.HasValue && take.HasValue)
            {   
                await JsonSerializer.SerializeAsync(stream,  ParseFileAsync<T>(path, skip.Value, take.Value));
            }
            else if (skip.HasValue)
            {
                await JsonSerializer.SerializeAsync(stream, ParseFileAsync<T>(path, skip.Value));
            }
            else
            {
                await JsonSerializer.SerializeAsync(stream, ParseFileAsync<T>(path));
            }
            using var reader = new StreamReader(stream);
            stream.Position = 0;
            return await reader.ReadToEndAsync();
        }


        private static Func<string, T[]> GetParser<T>() where T : notnull
        {
            var key = typeof(T);
            if (!_parsers.TryGetValue(key, out var value))
                value = AddToHash(_parsers, key, type => CreateParser(type));
            return input =>
            {
                return ((Func<string[], T[]>)value.del)(input.ToStructuredArray<string>(value.delimeters, value.sequenceOptions, 0, 0));
            };
        }

        private static (Delegate del, string[] delimiters, SequenceOptions sequenceAlgorithm) AddToHash(ConcurrentDictionary<Type, (Delegate, string[], SequenceOptions)> hash, Type key, Func<Type, (Delegate, string[], SequenceOptions)> func)
        {
            return hash.GetOrAdd(key, type =>
            {
                var del = func(type);
                hash[key] = del;
                return del;
            });
        }

        private static (Delegate, string[], SequenceOptions) CreateParser(Type type)
        {
            RetrieveDelimiterAndSyntaxValueFromType(type, out string[]? syntaxDelimiters, out string[]? syntax, out SequenceAlgorithm sequenceAlgorithm);

            var activator = BuildCreateInstanceFunction(type, syntax).Compile();
            return (activator, syntaxDelimiters, new SequenceOptions { SequenceAlgorithm = sequenceAlgorithm, PropertyCount = type.GetProperties().Count()});
        }

        private static void RetrieveDelimiterAndSyntaxValueFromType(Type type, out string[]? syntaxDelimiter, out string[]? syntax, out SequenceAlgorithm sequenceAlgorithm)
        {
            syntaxDelimiter = null;
            var attributes = type.GetCustomAttributes().ToArray();
            var syntaxFromAttribute = ((SyntaxAttribute?)attributes.FirstOrDefault(a => a.GetType() == typeof(SyntaxAttribute)));
            var delimiterSyntax = ((SingleDelimiterSyntaxAttribute?)attributes.FirstOrDefault(a => a.GetType() == typeof(SingleDelimiterSyntaxAttribute)));
            syntax = null;
            if (syntaxFromAttribute?.Value is not null)
            {
                syntaxDelimiter = ConstantRegex.SyntaxDelimiter.Matches(syntaxFromAttribute.Value).Select(m => m.Value).ToArray();
                sequenceAlgorithm = SequenceAlgorithm.Naive;
                syntax = syntaxFromAttribute?.Value.ToStructuredArray<string>(syntaxDelimiter, new SequenceOptions { SequenceAlgorithm = sequenceAlgorithm }, 0, 0);
              
            }
            else if (delimiterSyntax is not null)
            {
                syntaxDelimiter = Enumerable.Repeat(delimiterSyntax.Value, type.GetProperties().Length).ToArray();
                syntax = type.GetProperties().Select(p => p.Name).ToArray();
                sequenceAlgorithm = SequenceAlgorithm.Naive;
            }
            else
            {
                throw new InvalidOperationException($"Please provide a syntaxAttribute for type: {type.FullName}");
            }
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
                    var parse = Expression.Call(typeof(StringExtensions), nameof(StringExtensions.ToStructuredArray), new[] { arrayElementType }, new Expression[] { memberValueAsString, Expression.NewArrayInit(typeof(string), new[] { Expression.Constant(",") }), Expression.Constant(new SequenceOptions { SequenceAlgorithm = SequenceAlgorithm.Exact}),Expression.Constant(1), Expression.Constant(1)});
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

    internal struct SequenceOptions
    {
        internal SequenceAlgorithm SequenceAlgorithm { get; set; }
        internal int? PropertyCount { get; set; }
    }
}


