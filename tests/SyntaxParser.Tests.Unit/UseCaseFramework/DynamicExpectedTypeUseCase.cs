using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SyntaxParser.Tests.Unit.Extensions;
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

        public object? InvokeMethod(Type staticClassType, string methodName, object[] parameterValues)
        {
            var methodInfo = staticClassType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var genericArguments = new Type[] { Expected.Type.GetElementType() ?? Expected.Type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, parameterValues);
        }

        public object? InvokeMethod<T>(T instance, string methodName, object[] parameterValues)
        {
            var methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
            var genericArguments = new Type[] { Expected.Type.GetElementType() ?? Expected.Type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(instance, parameterValues);
        }

        public async Task<object?> InvokeMethodAsync(Type staticClassType, string methodName, object[] parameterValues)
        {
            var methodInfo = staticClassType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var genericArguments = new Type[] { Expected.Type.GetElementType() ?? Expected.Type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return await genericMethodInfo?.InvokeAsync(null, parameterValues);
        }

        public async Task<object?> InvokeMethodAsync<T>(T instance, string methodName, object[] parameterValues)
        {
            var methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var genericArguments = new Type[] { Expected.Type.GetElementType() ?? Expected.Type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return await genericMethodInfo?.InvokeAsync(instance, parameterValues);
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
