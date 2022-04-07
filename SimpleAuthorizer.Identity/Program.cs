using Microsoft.AspNetCore.Identity;
using SimpleAuthorizer.Common;
using SimpleAuthorizer.Common.Services;
using SimpleAuthorizer.Identity;
using SimpleAuthorizer.Identity.Infrastructure;
using SimpleAuthorizer.Identity.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .Configure<IdentitySettings>(builder.Configuration.GetSection(nameof(IdentitySettings)))
    .Configure<JwtTokenSettings>(options => builder.Configuration.GetSection("JwtTokenSettings").Bind(options));

builder.Services
    .AddTransient<IDataSeeder, IdentityDataSeeder>()
    .AddTransient<IIdentityService, IdentityService>()
    .AddDatabase<SimpleIdentityDbContext>(builder.Configuration)
    .AddApplication(Assembly.GetExecutingAssembly())
    .AddJWTAuthentication(builder.Configuration);

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SimpleIdentityDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Initialize();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
