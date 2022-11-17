using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Xunit.Abstractions;

namespace SyntaxParser.Tests.Shared.UseCaseFramework
{
    public interface IUseCase : IXunitSerializable
    {
        public void IsResultAsExpected(object producedResult, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>? options = null);
    }
    public class Dynamic
    {
        public Dynamic(object value)
        {
            Value = value;
            Type = value.GetType().FullName;
        }

        public string Type { get; set; }
        public object Value { get; set; }
    }

    public abstract class UseCase<TInput, TExpected> : IUseCase
    {
        public abstract TInput Input { get; set; }
        public abstract TExpected Expected { get; set; }

        public void IsResultAsExpected(object producedResult, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>? options = null)
        {
            IsResultAsExpected(producedResult, e => e, options);
        }

        public void IsResultAsExpected(object producedResult, Func<TExpected, object?> parse, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>? options = null)
        {
            if (options == null)
            {
                producedResult.Should().BeEquivalentTo(parse(Expected));
            }
            else
            {
                producedResult.Should().BeEquivalentTo(parse(Expected), options);
            }
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Input = JsonSerializer.Deserialize<TInput>(info.GetValue<string>(nameof(Input)));
            Expected = JsonSerializer.Deserialize<TExpected>(info.GetValue<string>(nameof(Expected)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Input), JsonSerializer.Serialize(Input));
            info.AddValue(nameof(Expected), JsonSerializer.Serialize(Expected));
        }


    }
}
