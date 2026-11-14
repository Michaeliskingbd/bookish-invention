using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace LibraryApi.Data;

public static class OpenIddictClientSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

        var manager = scope.ServiceProvider
            .GetRequiredService<IOpenIddictApplicationManager>();

        const string clientId = "bruno-client";

        var existingClient = await manager.FindByClientIdAsync(clientId);

        if (existingClient is not null)
        {
            return;
        }

        var descriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = clientId,
            DisplayName = "Bruno Development Client",
            ClientType = OpenIddictConstants.ClientTypes.Public
        };

        descriptor.RedirectUris.Add(
            new Uri("https://oauth.usebruno.com/callback"));

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Endpoints.Authorization);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Endpoints.Token);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.GrantTypes.RefreshToken);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.ResponseTypes.Code);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Scopes.Profile);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Scopes.Email);

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Prefixes.Scope + "offline_access");

        descriptor.Permissions.Add(
            OpenIddictConstants.Permissions.Prefixes.Scope + "openid");

        descriptor.Requirements.Add(
            OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange);

        await manager.CreateAsync(descriptor);
    }
}