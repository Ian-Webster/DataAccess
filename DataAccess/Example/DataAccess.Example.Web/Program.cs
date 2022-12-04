using DataAccess.Example.Data.DatabaseContexts;
using DataAccess.Example.Data.Repositories;
using DataAccess.Example.Web.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// get connection strings
var msSqlConnectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// set up db context
var databaseOptions = builder.Configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

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
builder.Services.AddScoped<RepositoryFactory<LibraryDatabaseContext>>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
