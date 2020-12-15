namespace Contentful.Statiq
{
    /// <summary>
    /// Keys of well-known Statiq document metadata for Contentful.
    /// </summary>
    public static class ContentfulKeys
    {
        private const string Root = "Contentful";

        /// <summary>
        /// The key used in Statiq documents to store the Contentful item.
        /// </summary>
        public const string ContentfulItem = Root + ".Item";

        /// <summary>
        /// Keys of well-known Statiq document metadata for Contentful system properties.
        /// </summary>
        public static class System
        {
            /// <summary>
            /// The environment of the item.
            /// </summary>
            public const string Environment = ContentfulItem + ".System.Environment";

            /// <summary>
            /// The status of the item.
            /// </summary>
            public const string Status = ContentfulItem + ".System.Status";

            /// <summary>
            /// Who archived the item.
            /// </summary>
            public const string ArchivedBy = ContentfulItem + ".System.ArchivedBy";

            /// <summary>
            /// Archived version of the item.
            /// </summary>
            public const string ArchivedVersion = ContentfulItem + ".System.ArchivedVersion";

            /// <summary>
            /// When the item was archieved.
            /// </summary>
            public const string ArchivedAt = ContentfulItem + ".System.ArchivedAt";

            /// <summary>
            /// Date of the first publication of the item.
            /// </summary>
            public const string FirstPublishedAt = ContentfulItem + ".System.FirstPublishedAt";

            /// <summary>
            /// How many times the item has publish.
            /// </summary>
            public const string PublishCounter = ContentfulItem + ".System.PublishCounter";

            /// <summary>
            /// Who published the item.
            /// </summary>
            public const string PublishedBy = ContentfulItem + ".System.PublishedBy";

            /// <summary>
            /// The version of the item.
            /// </summary>
            public const string PublishedVersion = ContentfulItem + ".System.PublishedVersion";

            /// <summary>
            /// How many times the item has been published.
            /// </summary>
            public const string PublishedCounter = ContentfulItem + ".System.PublishedCounter";

            /// <summary>
            /// The space of the item
            /// </summary>
            public const string Space = ContentfulItem + ".System.Space";

            /// <summary>
            /// The content type of the item.
            /// </summary>
            public const string ContentType = ContentfulItem + ".System.ContentType";

            /// <summary>
            /// The locale of the item.
            /// </summary>
            public const string Locale = ContentfulItem + ".System.Locale";

            /// <summary>
            /// When the item was deleted.
            /// </summary>
            public const string DeletedAt = ContentfulItem + ".System.DeletedAt";

            /// <summary>
            /// Who updated the item.
            /// </summary>
            public const string UpdatedBy = ContentfulItem + ".System.UpdatedBy";

            /// <summary>
            /// When the item was updated.
            /// </summary>
            public const string UpdatedAt = ContentfulItem + ".System.UpdatedAt";

            /// <summary>
            /// Who created the item.
            /// </summary>
            public const string CreatedBy = ContentfulItem + ".System.CreatedBy";

            /// <summary>
            /// When the item was created.
            /// </summary>
            public const string CreatedAt = ContentfulItem + ".System.CreatedAt";

            /// <summary>
            /// Version of the item.
            /// </summary>
            public const string Version = ContentfulItem + ".System.Version";

            /// <summary>
            /// Revision of the item.
            /// </summary>
            public const string Revision = ContentfulItem + ".System.Revision";

            /// <summary>
            /// Type of the item.
            /// </summary>
            public const string Type = ContentfulItem + ".System.Type";

            /// <summary>
            /// Link type.
            /// </summary>
            public const string LinkType = ContentfulItem + ".System.LinkType";

            /// <summary>
            /// Id of the item.
            /// </summary>
            public const string Id = ContentfulItem + ".System.Id";

            /// <summary>
            /// Organization to which the item belongs.
            /// </summary>
            public const string Organization = ContentfulItem + ".System.Organization";

            /// <summary>
            /// Usage period of the item.
            /// </summary>
            public const string UsagePeriod = ContentfulItem + ".System.UsagePeriod";
        }
    }
}