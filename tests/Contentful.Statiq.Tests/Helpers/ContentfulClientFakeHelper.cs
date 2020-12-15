using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using FakeItEasy;
using System.Threading;

namespace Contentful.Statiq.Tests.Helpers
{
    public static class ContentfulClientFakeHelper
    {
        public static IContentfulClient WithFakeContent<TContent>(this IContentfulClient client, params TContent[] content)
        {
            var response = new ContentfulCollection<TContent> { Items = content };

            A.CallTo(() => client.GetEntriesByType(A<string>.Ignored, A<QueryBuilder<TContent>>.Ignored, A<CancellationToken>.Ignored))
                .ReturnsLazily(() => response);

            return client;
        }
    }
}