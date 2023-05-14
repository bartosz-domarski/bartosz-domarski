using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Middleware;
using RestaurantAPI.Models;
using RestaurantAPI.Models.Validators;
using RestaurantAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;

[assembly: InternalsVisibleTo("RestaurantAPI.IntegrationTests")]

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("hasNationality", builder => builder.RequireClaim("Nationality"));
    options.AddPolicy("atleast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
    options.AddPolicy("atleast2Restaurants", builder => builder.AddRequirements(new MinimumRestaurantsRequirement(2)));
});
builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumRestaurantsRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<Restaurantseeder>();

builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<RestaurantQuery>, RestaurantQueryValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policy =>
        policy.WithOrigins(builder.Configuration["allowedOrigins"]).AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Restaurantseeder>();

seeder.Seed();

app.UseStaticFiles();

app.UseCors("FrontEndClient");

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
