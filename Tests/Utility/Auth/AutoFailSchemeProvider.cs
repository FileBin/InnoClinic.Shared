namespace InnoClinic.Shared.Tests.Utility.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

public class AutoFailSchemeProvider : AuthenticationSchemeProvider {
    public const string AutoFailScheme = "AutoFail";

    public AutoFailSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options) { }

    protected AutoFailSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes)
        : base(options, schemes) { }

    public override Task<AuthenticationScheme?> GetSchemeAsync(string name) {
        // if this matches the OIDC scheme, call the auth provider for whichever fake one we setup for the client
        if (name == "oidc") {
            return base.GetSchemeAsync(AutoFailScheme);
        }

        return base.GetSchemeAsync(name);
    }
}
