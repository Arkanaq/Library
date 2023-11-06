using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using MongoDB.Driver;
using Library.DbModel;
using Library.DbModel.Models;
using Library.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Author>("Authors").EntityType.HasKey(x=>x.Id);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddOData(options => options.Select().Filter().Count().OrderBy().Expand()
    .SetMaxTop(100).AddRouteComponents(
        "odata", modelBuilder.GetEdmModel()));

builder.Services.AddDbContext<MongoDbContext>(options =>
{
    var conString = builder.Configuration.GetConnectionString("MongoDb");
    var databaseName = builder.Configuration.GetValue<string>("DatabaseName");

    if (string.IsNullOrEmpty(conString) || string.IsNullOrEmpty(databaseName))
    {
        Console.WriteLine("");
        Environment.Exit(0);
    }
    options.UseMongoDB(conString, databaseName);
});

builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddScoped<UserService>();

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
