namespace InnoClinic.Shared.Tests;

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class AutoFail : AuthenticationHandler<AutoFailOptions> {
    public const string SchemeName = "AutoFail";

    public AutoFail(IOptionsMonitor<AutoFailOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
        return Task.FromResult(AuthenticateResult.Fail("AutoFail"));
    }
}

public class AutoFailOptions : AuthenticationSchemeOptions { }
