using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Statiq.Tests.Helpers;
using Contentful.Statiq.Tests.Models;
using FakeItEasy;
using FluentAssertions;
using Statiq.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Contentful.Statiq.Tests
{
    public class When_executing_Contentful_pipeline
    {
        [Fact]
        public async Task It_should_copy_all_system_fields_into_the_document()
        {
            // Arrange
            var content = new BlogPost
            {
                Sys = new SystemProperties
                {
                    Id = "xyz",
                    FirstPublishedAt = new DateTime(2020, 12, 31),
                },
                Title = "Blog post",
                Image = new Asset
                {
                    Title = "Image title",
                }
            };

            var contentfulClient = A.Fake<IContentfulClient>()
                .WithFakeContent(content);

            var sut = new Contentful<BlogPost>(contentfulClient, "test_ct");

            // Act
            var engine = EngineHelper.GetTestEngine(sut, docs =>
            {
                docs.Should().NotBeNull();
                return Task.CompletedTask;
            });
            await engine.ExecuteAsync();

            // Assert
            var outputs = engine.Outputs.FromPipeline(EngineHelper.TestPipeline);
            outputs.Should().HaveCount(1);

            var metadata = outputs[0];
            // TODO: Maybe add more fields?
            metadata.Get<string>(ContentfulKeys.System.Id).Should().Be("xyz");
            metadata.Get<DateTime>(ContentfulKeys.System.FirstPublishedAt).Should().Be(new DateTime(2020, 12, 31));
        }

        [Fact]
        public async Task It_should_set_the_default_content()
        {
            // Arrange
            var content = new BlogPost { Title = "Hello world!" };

            var contentfulClient = A.Fake<IContentfulClient>()
                .WithFakeContent(content);

            var sut = new Contentful<BlogPost>(contentfulClient, "test_ct")
                .WithContent(item => item.Title);

            // Set up the Assert
            var engine = EngineHelper.GetTestEngine(sut, async docs =>
            {
                var content = await docs[0].GetContentStringAsync();
                content.Should().Contain("world");
            });

            // Act & Assert
            await engine.ExecuteAsync();
        }
    }
}