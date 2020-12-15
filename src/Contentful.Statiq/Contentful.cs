using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contentful.Statiq
{
    /// <summary>
    /// Retrieves content items from Contentful.
    /// </summary>
    public sealed class Contentful<TContentModel> : Module where TContentModel : class
    {
        /// <summary>
        /// The code name of the field uses to fill the main Content field on the Statiq document. This is mostly useful for untyped content.
        /// </summary>
        public Func<TContentModel, string> GetContent { get; set; }

        private readonly IContentfulClient _client;
        private readonly string _contentTypeId;

        internal Action<QueryBuilder<TContentModel>> ConfigureQueryBuilder { get; set; } = (_ => { });

        /// <summary>
        /// Create a new instance of the Contentful module for Statiq using an existing Contentful client.
        /// </summary>
        /// <param name="client">The Contentful client to use.</param>
        /// <param name="contentTypeId">The Contentful contet type id.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="client"/> is null.</exception>
        public Contentful(IContentfulClient client, string contentTypeId)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client), $"{nameof(client)} must not be null");
            _contentTypeId = contentTypeId ?? throw new ArgumentNullException(nameof(contentTypeId), $"{nameof(contentTypeId)} must not be null");
        }

        /// <inheritdoc />
        protected override async Task<IEnumerable<IDocument>> ExecuteContextAsync(IExecutionContext context)
        {
            var qb = QueryBuilder<TContentModel>.New;
            ConfigureQueryBuilder?.Invoke(qb);

            var items = await _client.GetEntriesByType(_contentTypeId, qb);
            var documentTasks = items.Items.Select(item => ContentfulDocumentHelpers.CreateDocument(context, item, GetContent)).ToArray();

            return await Task.WhenAll(documentTasks);
        }
    }
}