using System;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace SyntaxParser.Tests.Unit.UseCaseFramework
{
    public abstract class DynamicExpectedTypeUseCase<TInput> : IUseCase
    {
        public abstract TInput Input { get; set; }
        public abstract Dynamic Expected { get; set; }

        public void IsResultAsExpected(object producedResult, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>? options = null)
        {
            IsResultAsExpected(producedResult, e => e, options);
        }

        public void IsResultAsExpected(object producedResult, Func<object, object?> parse, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>>? options = null)
        {
            var methodInfo = typeof(JToken).GetMethod(nameof(JToken.ToObject), 1, BindingFlags.Instance | BindingFlags.Public, null,
                new Type[] { }, null);
            var genericArguments = new[] { Expected.Type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            var expectedValue = genericMethodInfo?.Invoke(Expected.Value, new object[] { });
            if (options == null)
            {
                producedResult.Should().BeEquivalentTo(parse(expectedValue));
            }
            else
            {
                producedResult.Should().BeEquivalentTo(parse(expectedValue), options);
            }
        }



        public void Deserialize(IXunitSerializationInfo info)
        {
            Input = JsonConvert.DeserializeObject<TInput>(info.GetValue<string>(nameof(Input)));
            Expected = JsonConvert.DeserializeObject<Dynamic>(info.GetValue<string>(nameof(Expected)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Input), JsonConvert.SerializeObject(Input));
            info.AddValue(nameof(Expected), JsonConvert.SerializeObject(Expected));
        }
    }
}
