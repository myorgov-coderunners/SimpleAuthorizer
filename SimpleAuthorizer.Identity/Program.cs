using SimpleAuthorizer.Common;
using SimpleAuthorizer.Identity.Infrastructure;
using SimpleAuthorizer.Identity.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabase<SimpleIdentityDbContext>(builder.Configuration);
builder.Services.AddApplication(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.Configure<JwtTokenSettings>(
    options => builder.Configuration.GetSection("JwtToken").Bind(options));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Initialize();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
