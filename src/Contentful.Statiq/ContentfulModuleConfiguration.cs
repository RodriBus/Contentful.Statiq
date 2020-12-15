using Contentful.Core.Search;
using System;

namespace Contentful.Statiq
{
    /// <summary>
    /// Extension methods to configure the Contentful.Statiq module.
    /// </summary>
    public static class ContentfulModuleConfiguration
    {
        /// <summary>
        /// Get a string to use as content in the Statiq document.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="field">A func that returns a string to be used as content.</param>
        /// <typeparam name="TContentModel">The content model type.</typeparam>
        /// <returns>The module.</returns>
        public static Contentful<TContentModel> WithContent<TContentModel>(this Contentful<TContentModel> module, Func<TContentModel, string> field) where TContentModel : class
        {
            module.GetContent = field;
            return module;
        }

        /// <summary>
        /// Sets the ordering for retrieved content items.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="configureQueryBuilder">Query builder configuration action.</param>
        /// <typeparam name="TContentModel">The content model type.</typeparam>
        /// <returns>The module.</returns>
        public static Contentful<TContentModel> WithQuery<TContentModel>(this Contentful<TContentModel> module, Action<QueryBuilder<TContentModel>> configureQueryBuilder) where TContentModel : class
        {
            module.ConfigureQueryBuilder = configureQueryBuilder;
            return module;
        }
    }
}