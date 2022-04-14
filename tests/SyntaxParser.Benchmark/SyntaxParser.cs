using SyntaxParser;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SyntaxParser.Benchmark
{
    internal class SyntaxParser<T>
	{
		private Regex _delimeterMatch = new Regex(@"[^a-zA-Z0-9 ]*", RegexOptions.Compiled);
        private readonly Regex _delimeters;
        private readonly Dictionary<int, Action<T, string>> _fill;
        private readonly SyntaxParser.SyntaxParser<T> _coreParser;

		internal SyntaxParser()
		{
			var t = typeof(T);
			var syntax = ((SyntaxAttribute?)t.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(SyntaxAttribute)));
			if (syntax?.Value == null) throw new InvalidOperationException();
			_delimeters = new Regex(string.Join("|",
				 ConstantRegex.Symbols.Matches(syntax.Value).Where(m => !string.IsNullOrEmpty(m.Value))
					.SelectMany(m => m.Captures.Select(c => c.Value)).ToArray()), RegexOptions.Compiled);
			_fill = _delimeters.Split(syntax.Value).Select((val, index) => new { Index = index, Value = val })
				.ToDictionary(i => i.Index, i =>
				{
					return SetPropertyValue(i.Value);
				});
			_coreParser = new SyntaxParser.SyntaxParser<T>();
		}

		internal IEnumerable<T> Parse(string text)
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
				result[i] = Fill(line);
			}
			return result;
		}

		internal IEnumerable<T> ParseLines(string path)
		{
			var lines = File.ReadAllLines(path);
			var result = new T[lines.Length];
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				if (string.IsNullOrWhiteSpace(line)) continue;
				result[i] = Fill(line);
			}
			return result;
		}

		internal IEnumerable<T> ParseFile(string path)
		{
			var result = new List<T>();
			using var sr = new StreamReader(path);
			while (sr.Peek() >= 0)
			{
				string? line = sr.ReadLine();
				if (string.IsNullOrWhiteSpace(line)) continue;
				result.Add(Fill(line));
			}
			return result;
		}

		private T Fill(string line)
		{
			var result = Activator.CreateInstance<T>();
			var values = _delimeters.Split(line);
			for (int i = 0; i < values.Length; i++)
			{
				_fill[i](result, values[i]);
			}
			return result;
		}

		private static Action<T, string> SetPropertyValue(string name)
		{
			var field = typeof(T).GetProperty(name);
			return (target, value) => field.SetValue(target, value.Trim());
		}
	}
}