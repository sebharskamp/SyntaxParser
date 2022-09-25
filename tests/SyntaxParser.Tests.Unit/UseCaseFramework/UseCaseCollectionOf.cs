using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Xunit.Abstractions;
using Newtonsoft.Json;

namespace SyntaxParser.Tests.Unit.UseCaseFramework
{
    public abstract class UseCaseCollectionOf<T> : IEnumerable<T[]>
    {
        protected abstract List<T> UseCases { get; }

        public IEnumerator<T[]> GetEnumerator() => UseCases?.Select(d => new[] { d }).GetEnumerator() ?? throw new InvalidOperationException("Set test data.");

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
