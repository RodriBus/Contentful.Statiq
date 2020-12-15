using Statiq.Common;
using Statiq.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contentful.Statiq.Tests.Helpers
{
    public static class EngineHelper
    {
        internal const string TestPipeline = "test";

        internal static Engine GetTestEngine<TContent>(Contentful<TContent> module, Func<IReadOnlyList<IDocument>, Task> finalModule) where TContent : class
        {
            var engine = new Engine();
            var pipeline = new Pipeline()
            {
                InputModules = {
                    module,
                    new TestModule(finalModule)
                }
            };

            engine.Pipelines.Add(TestPipeline, pipeline);
            return engine;
        }
    }
}