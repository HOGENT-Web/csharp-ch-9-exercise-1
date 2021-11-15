using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Client.Infrastructure
{
    public class FakeAuthenticationProvider : AuthenticationStateProvider
    {
        public static ClaimsPrincipal Anonymous => new(new ClaimsIdentity());
        public static ClaimsPrincipal Administrator => new(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Fake Administrator"),
            new Claim(ClaimTypes.Email, "fake-administrator@gmail.com"),
            new Claim(ClaimTypes.Role, Roles.Administrator),
        }, "Fake Authentication"));

        public static ClaimsPrincipal Customer => new(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Fake Customer"),
            new Claim(ClaimTypes.Email, "fake-customer@gmail.com"),
            new Claim(ClaimTypes.Role, Roles.Customer),
        }, "Fake Authentication"));

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(Administrator));
        }
    }
}
