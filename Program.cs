using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Services;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddControllers();


builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorization();

builder.Services.AddAuthorization();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));

    options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<AppDbContext>();
    })
    .AddServer(options =>
    {
        // Define the authorization and token endpoints..
        options.SetAuthorizationEndpointUris("/connect/authorize");
        options.SetTokenEndpointUris("/connect/token");

        // Enable Authorization Code Flow with mandatory PKCE.
        options.AllowAuthorizationCodeFlow();
        options.RequireProofKeyForCodeExchange();

        // Enable refresh tokens.
        options.AllowRefreshTokenFlow();

        // Register the scopes clients may request.
        options.RegisterScopes(
            "openid",
            "profile",
            "email",
            "roles",
            "offline_access"
        );

        // Development certificates only.
        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        // Integrate OpenIddict with ASP.NET Core.
        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough();
    }).AddValidation(options =>
{
    // Reuse the local authorization server configuration.
    options.UseLocalServer();

    // Integrate token validation with ASP.NET Core.
    options.UseAspNetCore();
});

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDataProtection();

builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 12;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();


var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await OpenIddictClientSeeder.SeedAsync(app.Services);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();



// dotnet new webapi -n LibraryApi
// cd LibraryApi
// dotnet run