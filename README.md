# Contentful.Statiq
[Statiq](https://Statiq.dev) module to retrieve content from the [Contentful API](https://www.contentful.com/) to be used as part of a Statiq pipeline.

## Installation
### Add the package
You can add the NuGet package to your project:
- Using the package console:
```
Install-Package Contentful.Statiq
```
- Using the donet CLI:
```
dotnet add package Contentful.Statiq
```
- Or search for Contentful.Statiq through the NuGet Package Manager in Visual Studio.

### (Optional) Generate your content type models
If you want to auto-generate classes representing your Contentful space Content-Type definitions, you may want to check out the [RodriBus/Contentful.ModelGenerator.Cli](https://github.com/RodriBus/Contentful.ModelGenerator.Cli) project or the [Contentful/dotnet-models-creator-cli](https://github.com/contentful/dotnet-models-creator-cli) project.

## Usage
With the package installed you can now use the Contentful module in a Statiq pipeline to retrieve some content.
These are the minimum steps to get it running:

### Define your content-type
Whether auto-generated or handcrafted, you will need to define a class representing your content-type. This is an example:
```csharp
using Contentful.Core.Models;

public class BlogPost
{
    public const string Id = "blogPost";
    public SystemProperties Sys { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Body { get; set; }
    public Asset Cover{ get; set; }
}
```

#### (Optional) Add a Contentful API Client
If you don't have one already, you can configure a new `IContentfulClient` into the DI container:
```csharp
using Contentful.Statiq;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task<int> Main(string[] args) =>
        await Bootstrapper
        .Factory
        .CreateDefault(args)
        .ConfigureServices((services, settings) => {
            services.AddContentful((IConfiguration) settings);
        })
        .AddPipelines()
        .RunAsync();
}
```
This will read the application configuration using the [options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1) and configure a ready-to-use `IContentfulClient` to be requested from the DI container.
You will need to add to your [configuration file](https://statiq.dev/framework/configuration/settings#specifying-settings-with-the-bootstrapper) the following variables. This example is for a json configuration format:
```json
"ContentfulOptions": {
  "SpaceId": "",
  "DeliveryApiKey": "",
  "PreviewApiKey": "",
  "Environment": "master",
  "ResolveEntriesSelectively": false,
  "UsePreviewApi": false
}
```
You can get more details about this configuration at the [Contentful.Net](https://github.com/contentful/contentful.net) repository

Also, you may want to define a [content type resolver](https://www.contentful.com/developers/docs/net/tutorials/using-net-cda-sdk/#get-entries-of-multiple-types-or-by-interface) for the client. To do so you may add one to the services configuration like this:
```csharp
using Contentful.Statiq;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task<int> Main(string[] args) =>
        await Bootstrapper
        .Factory
        .CreateDefault(args)
        .ConfigureServices((services, settings) => {
            services.AddContentful((IConfiguration) settings, new MyEntityResolver());
        })
        .AddPipelines()
        .RunAsync();
}
```

### Use the module in a pipeline
Finally, add the module as an input module in a pipeline.
This is an example of retrieving data from the API and using it with razor templates to render the pages:

```csharp
using Contentful.Statiq;

public class Blogs : Pipeline
{
    public Blogs(IContentfulClient client)
    {
        InputModules = new ModuleList
        {
            // Retrieve data
            new Contentful<BlogPost>(client, BlogPost.Id),
        };

        ProcessModules = new ModuleList
        {
            // Include blog view
            new MergeContent(new ReadFiles("BlogPost.cshtml")),

            // Render page
            new RenderRazor(),

            // Set destination
            new SetDestination(Config.FromDocument(doc =>
                new NormalizedPath($"/blog/{doc.AsContentful<BlogPost>().Slug}.html"))),
        };

        OutputModules = new ModuleList
        {
            // Write the rendered files to the destination path
            new WriteFiles(),
        };
    }
}
```

## Setting default content
It could be useful to define the content of the input contentful documents based on the data fetched from the API.
To do so you can define the content of the documents using the following method:

```csharp
...
InputModules = new ModuleList
{
    new Contentful<BlogPost>(client, BlogPost.Id)
        .WithContent(post => post.Body),
};
...
```

## Advanced filtering
If you want to define a query to filter the results from the Contentful API, you can access the `QueryBuilder<T>` of the module using the following method:
```csharp
...
InputModules = new ModuleList
{
    new Contentful<BlogPost>(client, BlogPost.Id)
        .WithQuery(qb => qb.Include(10)),
};
...
```

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/RodriBus/Contentful.Statiq/tags). 

## Authors
* **Diego Rodríguez** - *Initial work* - [RodriBus](https://github.com/RodriBus)

See also the list of [contributors](https://github.com/RodriBus/Contentful.Statiq/contributors) who participated in this project.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments
This project is **heavily** inspired by the [Kontent.Statiq](https://github.com/alanta/Kontent.Statiq) and [Contentful.Wyam](https://github.com/contentful/contentful.wyam) libraries.