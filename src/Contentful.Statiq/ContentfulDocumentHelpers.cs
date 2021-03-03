using Contentful.Core.Models;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contentful.Statiq
{
    /// <summary>
    /// Helpers to construct Statiq IDocument instances from Contentful items.
    /// </summary>
    internal static class ContentfulDocumentHelpers
    {
        internal static void ThrowIfNull(this IDocument document, string fieldName)
        {
            if (document == null)
            {
                throw new ArgumentNullException(fieldName, "Document object is not expected to be null.");
            }
        }

        internal static IDocument CreateDocument<TContentModel>(IExecutionContext context, TContentModel item, Func<TContentModel, string> getContent) where TContentModel : class
        {
            var props = typeof(TContentModel)
                .GetProperties(BindingFlags.Instance
                    | BindingFlags.FlattenHierarchy
                    | BindingFlags.GetProperty
                    | BindingFlags.Public);

            var content = getContent?.Invoke(item);

            var doc = CreateDocumentInternal(context, item, props, content);
            return doc;
        }

        internal static IDocument CreateDocumentInternal(IExecutionContext context, object item, IEnumerable<PropertyInfo> props, string content)
        {
            var metadata = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(ContentfulKeys.ContentfulItem, item),
            };

            AddSystemProperties(item, props, metadata);

            return context.CreateDocument(metadata, content, null);
        }

        /// <summary>
        /// Add Contentful system properties to document metadata.
        /// </summary>
        private static void AddSystemProperties(object item, IEnumerable<PropertyInfo> props, IList<KeyValuePair<string, object>> metadata)
        {
            var sysProp = props.FirstOrDefault(prop => typeof(SystemProperties).IsAssignableFrom(prop.PropertyType))?.GetValue(item);
            if (sysProp is SystemProperties systemProp)
            {
                metadata.AddRange(new[]
                {
                    new KeyValuePair<string, object>(ContentfulKeys.System.Environment, systemProp.Environment),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Status, systemProp.Status),
                    new KeyValuePair<string, object>(ContentfulKeys.System.ArchivedBy, systemProp.ArchivedBy),
                    new KeyValuePair<string, object>(ContentfulKeys.System.ArchivedVersion, systemProp.ArchivedVersion),
                    new KeyValuePair<string, object>(ContentfulKeys.System.ArchivedAt, systemProp.ArchivedAt),
                    new KeyValuePair<string, object>(ContentfulKeys.System.FirstPublishedAt, systemProp.FirstPublishedAt),
                    new KeyValuePair<string, object>(ContentfulKeys.System.PublishCounter, systemProp.PublishCounter),
                    new KeyValuePair<string, object>(ContentfulKeys.System.PublishedBy, systemProp.PublishedBy),
                    new KeyValuePair<string, object>(ContentfulKeys.System.PublishedVersion, systemProp.PublishedVersion),
                    new KeyValuePair<string, object>(ContentfulKeys.System.PublishedCounter, systemProp.PublishedCounter),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Space, systemProp.Space),
                    new KeyValuePair<string, object>(ContentfulKeys.System.ContentType, systemProp.ContentType),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Locale, systemProp.Locale),
                    new KeyValuePair<string, object>(ContentfulKeys.System.DeletedAt, systemProp.DeletedAt),
                    new KeyValuePair<string, object>(ContentfulKeys.System.UpdatedBy, systemProp.UpdatedBy),
                    new KeyValuePair<string, object>(ContentfulKeys.System.UpdatedAt, systemProp.UpdatedAt),
                    new KeyValuePair<string, object>(ContentfulKeys.System.CreatedBy, systemProp.CreatedBy),
                    new KeyValuePair<string, object>(ContentfulKeys.System.CreatedAt, systemProp.CreatedAt),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Version, systemProp.Version),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Revision, systemProp.Revision),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Type, systemProp.Type),
                    new KeyValuePair<string, object>(ContentfulKeys.System.LinkType, systemProp.LinkType),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Id, systemProp.Id),
                    new KeyValuePair<string, object>(ContentfulKeys.System.Organization, systemProp.Organization),
                    new KeyValuePair<string, object>(ContentfulKeys.System.UsagePeriod, systemProp.UsagePeriod),
                });
            }
        }
    }
}