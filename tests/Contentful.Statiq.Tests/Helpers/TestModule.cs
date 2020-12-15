using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contentful.Statiq.Tests.Helpers
{
    internal class TestModule : Module
    {
        private Func<IReadOnlyList<IDocument>, Task> TestFunc { get; }

        public TestModule(Func<IReadOnlyList<IDocument>, Task> test)
        {
            TestFunc = test;
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
        {
            await TestFunc?.Invoke(context.Inputs);
            return context.Inputs;
        }
    }
}