using SimpleAuthorizer.API.Infrastructure;
using SimpleAuthorizer.Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabase<ApiDbContext>(builder.Configuration);
builder.Services.AddApplication(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Initialize();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
