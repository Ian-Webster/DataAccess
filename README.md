# DataAccess

## Introduction

A simple data access base repository that can be used in other projects

## Usage

Full example project can be found here https://github.com/Ian-Webster/sandbox/tree/main/nuget-samples/DataAccess.Sample

### Install NuGet packages

Install the following NuGet packges into your project
1. [DataAccess.Repository](https://github.com/Ian-Webster/DataAccess/pkgs/nuget/DataAccess.Repository)
2. [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/7.0.0)
3. [Microsoft.EntityFrameworkCore.Relational](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Relational/7.0.0)
4. Your choice of EF core database targeting package (for example https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer)

### Create entites and entity type configurations

1. Create your database entity classes, as an example a movie entity;
	```csharp
	public class Movie
	{
		public Guid MovieId { get; set; }
		public string Name { get; set; }
	}
	```
2. Create your entity type configuration classes, as an example an entity type configuration for the movie entity;
	```csharp
	public class MovieEntityTypeConfiguration: IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.HasKey(pk => pk.MovieId);
			builder.ToTable(nameof(Movie), SchemaNames.Public);
		}
	}
	```

### Create a database context class

You will need to create a database context class that inherits from [DbContext](https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0)

As an example here is the database context class for the Movie database;

```csharp
public class MovieContext: DbContext // inherit from DbContext
{
	// inject DbContextOptions and base to the base class
    public MovieContext(DbContextOptions options): base(options)
    {        
    }

	// override OnModelCreating so we can apply our entity type configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		// get the the assembly your EntityTypeConfigurations are in
		// in this case they are in the same assembly as this context class
        var assembly = Assembly.GetAssembly(typeof(MovieContext));
        if (assembly == null)
        {
            throw new Exception($"Failed to get assembly for {nameof(MovieContext)}");
        }       
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
```

### Create repository classes

As an example the repository class for the movie entity;

```csharp
using DataAccess.Repository;
using DataAccess.Sample.Data.DatabaseContexts;
using DataAccess.Sample.Domain.Entities;

public class MovieRepository: IMovieRepository
{
    private readonly IRepository<Movie> _movieRepository;
	// inject RepositoryFactory for creating IRepository instances
    public MovieRepository(RepositoryFactory<MovieContext> repositoryFactory)
    {
		// create an instance of IRepository for our entity (Movie)
        _movieRepository = repositoryFactory.GetRepositoryByType<Movie>();
    }

    public async Task<Movie?> GetMovieById(Guid movieId, CancellationToken token)
    {
		// perform a database read for the Movie entity
        return await _movieRepository.FirstOrDefault(m => m.MovieId == movieId, token);
    }
}
```

### Configure IoC

In your IoC bootstrapping you need to;
1. Add your database context, as an example here is the set up for the MovieContext connecting to a postgres database;
	```csharp
	builder.Services.AddDbContext<MovieContext>(options =>
	{
		options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
	});
	```
2. Set up your services;
	```csharp
	// add RepositoryFactory (this will be needed by your repository class)
	builder.Services.AddScoped<RepositoryFactory<MovieContext>>();
	// add your repositories
	builder.Services.AddScoped<IMovieRepository, MovieRepository>();
	```


## Version history

- 3.0.0
	- refactored repository factory to follow unit of work pattern
	- added new functionality to IRepository for optional take on List, projection and paging
	- version bumps to NuGetPackages
- 2.0.0 - work in progress commit to support the DataAccess.Repository.HotChocolate package
- 1.0.4 - added usage instructions to readme file
- 1.0.3 - updated main readme file
- 1.0.2 - changes to main build action to attempt to fix NuGet publishing issue
- 1.0.1 - testing NuGet publish on merge to main post pull request
- 1.0.0 - initial test publication of the NuGet package
