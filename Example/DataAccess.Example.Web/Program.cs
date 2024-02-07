using DataAccess.Example.Data.DatabaseContexts;
using DataAccess.Example.Data.Queries;
using DataAccess.Example.Data.Repositories;
using DataAccess.Example.Web.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DataAccess.Example.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // add documentation for controllers
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    
    // add documentation for models
    xmlFile = $"{typeof(Book).Assembly.GetName().Name}.xml";
    xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// get connection strings
var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// set up db context
var databaseOptions = builder.Configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

if (databaseOptions == null) throw new Exception("DatabaseOptions not set");

builder.Services.AddDbContext<LibraryDatabaseContext>(options =>
{
    if (databaseOptions.UseMsSql)
    {
        options.UseSqlServer(msSqlConnectionString);
    }
    else
    {
        options.UseNpgsql(postgresConnectionString);
    }
});

// set up services
builder.Services.AddLogging();
builder.Services.AddScoped<UnitOfWork<LibraryDatabaseContext>>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// set up HotChocolate
builder.Services.AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddTypeExtension<BookQuery>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // set dark mode to save everyone's eyes
        options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
