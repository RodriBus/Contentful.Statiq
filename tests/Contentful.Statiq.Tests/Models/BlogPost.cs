using Contentful.Core.Models;

namespace Contentful.Statiq.Tests.Models
{
    public class BlogPost
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        public Asset Image { get; set; }
    }
}