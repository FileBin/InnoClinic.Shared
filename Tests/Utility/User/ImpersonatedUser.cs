namespace InnoClinic.Shared.Tests.Utility.User;

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public abstract class ImpersonatedUser : AuthenticationHandler<ImpersonatedAuthenticationSchemeOptions> {
    protected ImpersonatedUser(IOptionsMonitor<ImpersonatedAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
        var claims = new List<Claim>();
        Options.Configure(claims);
        var identity = new ClaimsIdentity(claims, Options.OriginalScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Options.OriginalScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}
