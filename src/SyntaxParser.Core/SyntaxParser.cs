﻿using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SyntaxParser
{
	public class SyntaxParser<T>
	{
		private readonly Regex _delimeters;
		private readonly Func<string[], T> _createInstance;

		public SyntaxParser()
		{
			var t = typeof(T);
			var syntax = ((SyntaxAttribute?)t.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(SyntaxAttribute)));
			if (syntax?.Value == null) throw new InvalidOperationException();
			_delimeters = new Regex(string.Join("|",
				ConstantRegex.Symbols.Matches(syntax.Value).Where(m => !string.IsNullOrEmpty(m.Value))
					.SelectMany(m => m.Captures.Select(c => c.Value)).ToArray()), RegexOptions.Compiled);
			_createInstance = BuildCreateInstanceFunction(_delimeters.Split(syntax.Value));
		}

		public IEnumerable<T> ParseText(string text)
		{
			string[] lines = text.Split(
							new string[] { Environment.NewLine },
							StringSplitOptions.None
						);
			var result = new T[lines.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				if (string.IsNullOrWhiteSpace(line)) continue;
				result[i] = _createInstance(_delimeters.Split(line));
			}
			return result;
		}

		public IEnumerable<T> ParseFile(string path)
		{
			var result = new List<T>();
			using var sr = new StreamReader(path);
			while (sr.Peek() >= 0)
			{
				string? input = sr.ReadLine();
				if (string.IsNullOrWhiteSpace(input)) continue;
				result.Add(_createInstance(_delimeters.Split(input)));
			}
			return result;
		}

        public string ParseFileToJson(string path)
		{
			return JsonSerializer.Serialize(ParseFile(path));
		}

		public async IAsyncEnumerable<T> ParseFileAsync(string path)
		{
			var result = new List<T>();
			using var sr = new StreamReader(path);
			while (sr.Peek() >= 0)
			{
				string? input = await sr.ReadLineAsync();
				if (string.IsNullOrWhiteSpace(input)) continue;
				yield return _createInstance(_delimeters.Split(input));
			}
		}

		public async Task<string> ParseFileToJsonAsync(string path)
        {
			using var stream = new MemoryStream();
			await JsonSerializer.SerializeAsync(stream, ParseFileAsync(path));
			using var reader = new StreamReader(stream);
			stream.Position = 0;
			return reader.ReadToEnd();
		}

		private static Func<string[], T> BuildCreateInstanceFunction(string[] namesOrder)
		{
			var type = typeof(T);
			var instance = Expression.New(typeof(T));
			var propertyInfos = type.GetProperties().ToDictionary(pi => pi.Name);
			var propertyAssignments = new MemberAssignment[propertyInfos.Count()];

			var arrayExpression = Expression.Parameter(typeof(string[]), "params");

			for (int i = 0; i < propertyAssignments.Length; i++)
			{
				if (!propertyInfos.ContainsKey(namesOrder[i])) continue;

				var assignment = Expression.Bind(type.GetProperty(namesOrder[i]), Expression.ArrayIndex(arrayExpression, Expression.Constant(i)));
				propertyAssignments[i] = assignment;
			}

			var creationExpression = Expression.New(type);
			var initialization = Expression.MemberInit(creationExpression, propertyAssignments);

			var expression = Expression.Lambda(initialization, arrayExpression);

			return ((Func<string[], T>)expression.Compile());
		}
    }
}