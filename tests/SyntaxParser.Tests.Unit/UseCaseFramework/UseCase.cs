using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SyntaxParser.Tests.Unit.UseCaseFramework
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
            Type = value.GetType();
        }

        public Type Type { get; set; }
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
            Input = JsonConvert.DeserializeObject<TInput>(info.GetValue<string>(nameof(Input)));
            Expected = JsonConvert.DeserializeObject<TExpected>(info.GetValue<string>(nameof(Expected)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Input), JsonConvert.SerializeObject(Input));
            info.AddValue(nameof(Expected), JsonConvert.SerializeObject(Expected));
        }


    }
}
