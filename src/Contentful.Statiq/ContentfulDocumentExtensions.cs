using Statiq.Common;
using System;

namespace Contentful.Statiq
{
    /// <summary>
    /// Extensions for working with contentful documents.
    /// </summary>
    public static class ContentfulDocumentExtensions
    {
        /// <summary>
        /// Return the strong typed model for a Statiq document.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <typeparam name="TModel">The type of content to return.</typeparam>
        /// <returns>The strong typed content model.</returns>
        /// <exception cref="ArgumentNullException">Thrown when callen on a null document.</exception>
        /// <exception cref="InvalidOperationException">Thrown when callen on a document that isn't a Contentful content document.</exception>
        public static TModel AsContentful<TModel>(this IDocument document)
        {
            document.ThrowIfNull(nameof(document));

            if (document.TryGetValue(ContentfulKeys.ContentfulItem, out TModel contentItem))
            {
                return contentItem;
            }

            throw new InvalidOperationException($"This is not a Contentful document: {document.ToSafeDisplayString()}");
        }

        /// <summary>
        /// Return the strong typed model for a Statiq document.
        /// </summary>
        /// <param name="document">The Document.</param>
        /// <typeparam name="TModel">The type of content to return.</typeparam>
        /// <returns>The strong typed content model.</returns>
        /// <exception cref="ArgumentNullException">Thrown when callen on a null document.</exception>
        /// <exception cref="InvalidOperationException">Thrown when callen on a document that isn't a Contentful content document.</exception>
        public static bool IsContentful<TModel>(this IDocument document)
        {
            return document.TryGetValue(ContentfulKeys.ContentfulItem, out TModel _);
        }
    }
}