using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using SyntaxParser.Tests.Shared.Extensions;
using Xunit.Abstractions;

namespace SyntaxParser.Tests.Shared.UseCaseFramework
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
            object expectedValue = Expected.Value;
            if (Expected.Value is JsonElement element)
            {
                expectedValue = JsonSerializer.Deserialize(element, Type.GetType(Expected.Type));
            }

            if (options == null)
            {
                object? expectation = parse(expectedValue);
                producedResult.Should().BeEquivalentTo<object>(expectation);
            }
            else
            {
                producedResult.Should().BeEquivalentTo(parse(expectedValue), options);
            }
        }

        public object? InvokeMethod(Type staticClassType, string methodName, object[] parameterValues)
        {
            var methodInfo = staticClassType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var type = Type.GetType(Expected.Type);
            var genericArguments = new Type[] { type.GetElementType() ?? type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, parameterValues);
        }

        public object? InvokeMethod<T>(T instance, string methodName, object[] parameterValues)
        {
            var methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
            var type = Type.GetType(Expected.Type);
            var genericArguments = new Type[] { type.GetElementType() ?? type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(instance, parameterValues);
        }

        public async Task<object?> InvokeMethodAsync(Type staticClassType, string methodName, object[] parameterValues)
        {
            var methodInfo = staticClassType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var type = Type.GetType(Expected.Type);
            var genericArguments = new Type[] { type.GetElementType() ?? type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return await genericMethodInfo?.InvokeAsync(null, parameterValues);
        }

        public async Task<object?> InvokeMethodAsync<T>(T instance, string methodName, object[] parameterValues)
        {
            var methodInfo = typeof(T).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            var type = Type.GetType(Expected.Type);
            var genericArguments = new Type[] { type.GetElementType() ?? type };
            var genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return await genericMethodInfo?.InvokeAsync(instance, parameterValues);
        }


        public void Deserialize(IXunitSerializationInfo info)
        {
            Input = JsonSerializer.Deserialize<TInput>(info.GetValue<string>(nameof(Input)));
            Expected = JsonSerializer.Deserialize<Dynamic>(info.GetValue<string>(nameof(Expected)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Input), JsonSerializer.Serialize(Input));
            info.AddValue(nameof(Expected), JsonSerializer.Serialize(Expected));
        }
    }
}
