# HotChocolate Extension
## Introduction
This extension provides GraphQL query functionality via the [HotChocolate Library](https://chillicream.com/docs/hotchocolate/v13)

## Installation
1. You'll need to install and set up the core data access NuGet package (see instructions [here](https://github.com/Ian-Webster/DataAccess#usage))
2. Add the following NuGet packages to your IoC project;
    1. [HotChocolate](https://www.nuget.org/packages/HotChocolate/13.7.0?_src=template)
    2. [HotChocolate.AspNetCore](https://www.nuget.org/packages/HotChocolate.AspNetCore/13.7.0?_src=template)
    3. [HotChocolate.Data](https://www.nuget.org/packages/HotChocolate.Data/13.7.0?_src=template)
3. Add the following NuGet packages to your Repository project;
    1. [HotChocolate](https://www.nuget.org/packages/HotChocolate/13.7.0?_src=template)
    2. [HotChocolate.Data](https://www.nuget.org/packages/HotChocolate.Data/13.7.0?_src=template)
    3. [DataAccess.Repository](https://github.com/Ian-Webster/DataAccess/pkgs/nuget/DataAccess.Repository)
    4. [DataAccess.Repository.HotChocolate](https://github.com/Ian-Webster/DataAccess/pkgs/nuget/DataAccess.Repository.HotChocolate)
4. Modify your IoC services to add and configure HotChocolate;
	```csharp
   // set up HotChocolate
    builder.Services.AddGraphQLServer()
        .AddQueryType(q => q.Name("Query"))
        .AddProjections()
        .AddFiltering()
        .AddSorting(); 
    ```
 5.Modify your middleware configuration to include `app.MapGraphQL();` 
 6. We'll revisit your IoC configuration later to add queries.

## Building queries
Queries in GraphQL are like a stand-in for controller endpoints in REST, they provide a public API into your data, you can read more here https://chillicream.com/docs/hotchocolate/v13/defining-a-schema/queries

HotChocolate can query your DBContext directly should you wish, however it does also provide a number of IQueryable extension methods and a query context class that allow you to use your own services and repository methods to perform the data operations. 

The purpose of this extension library to obfuscate the complexities involved in modifying repository methods to support the HotChocolate extensions by providing a number of extension methods in the QueryExtensions;
* GetQueryItem - returns a single entity, supports filtering and projection
* GetQueryItems - returns a collection of entities, supports filtering, projection and sorting
* GetPagedQueryItems - returns a [Connection](https://chillicream.com/docs/hotchocolate/v13/fetching-data/pagination/#connections) object provided by the HotChocolate library, supports filtering, projection, sorting and pagination

Usage for this extension is as follows;
1. Modify your repository to accept IResolverContext and to call the extension, as an example this is how to get a single book entity for GraphQL;
    ```csharp
    // interface
    using HotChocolate.Resolvers; // namespace for IResolverContext
    namespace DataAccess.Example.Data.Repositories;
    
    public interface IBookRepository
    {
        Task<Book?> GetBookForGraphQuery(IResolverContext context, CancellationToken token);
    }

    // implementation
    using DataAccess.Repository.HotChocolate; // namespace for this extension
    using HotChocolate.Resolvers; // namespace for IResolverContext
    
    namespace DataAccess.Example.Data.Repositories;
    
    public class BookRepository: IBookRepository
    {
        private readonly IRepository<Book> _bookRepo;
    
        public BookRepository(RepositoryFactory<LibraryDatabaseContext> repositoryFactory)
        {
            _bookRepo = repositoryFactory.GetRepositoryByType<Book>();
        }
    
        public async Task<Book?> GetBookForGraphQuery(IResolverContext context, CancellationToken token)
        {
            return await _bookRepo.GetQueryItem(context, token);
        }
    }
    ```
2. Create a query class here is the book query;
    ```csharp
    using HotChocolate;
    using HotChocolate.Types;
    using HotChocolate.Resolvers;
    
    namespace DataAccess.Example.Data.Queries;
    
    [ExtendObjectType("Query")]
    public class BookQuery
    {
        [UseProjection] // enable projecton for this query
        [UseFiltering] // enable filtering for this query
        // [Service] is an attribute hint provided by the HotChocolate library
        // telling the code to use DI to retrieve the IBookRepository dependency
        // IResolverContext and CancellationToken objects are provide as part of the
        // GraphQL context
        public async Task<Book?> GetBook([Service] IBookRepository repository, IResolverContext context, CancellationToken token)
        {
            return await repository.GetBookForGraphQuery(context, token);
        }
    }
    ```
3. Modify the HotChocolate IoC configuration you added earlier to include your query;
    ```csharp
    // set up HotChocolate
    builder.Services.AddGraphQLServer()
        .AddQueryType(q => q.Name("Query"))
        .AddTypeExtension<BookQuery>() // adding the book query type
        .AddProjections()
        .AddFiltering()
        .AddSorting();
    ```
4. Run the API project as normal, you should be able to visit /graphql and see the [Banana Cake Pop](https://chillicream.com/products/bananacakepop/) UX
5. Run a query through the UX, as an example this will return the book "Fight Club" from the example project;
	```json
    {
        book (where: {bookId: {eq: "2c4a20f5-3fc8-4694-9c03-cb444bf416dc"}})
        {
            bookId,
            name
        }
    }
    ```

## Version history
* 2.0.0 - 
  * Updated to DataAccess.Repository 3.0.0
  * Updated to HotChocolate 13.8.1
* 1.1.1 - Fixed documenation issues
* 1.1.0 - Added offset paging
* 1.0.0 - Initial release
* 0.1.0 - Initial project creation, getting everything figured out and running locally 
