namespace InnoClinic.Shared.Tests.Utility.Auth;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

public class InterceptOidcAuthenticationSchemeProvider : AuthenticationSchemeProvider {
    public const string InterceptedScheme = "InterceptedScheme";

    public InterceptOidcAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options) {
    }

    protected InterceptOidcAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes)
        : base(options, schemes) {
    }

    public override Task<AuthenticationScheme?> GetSchemeAsync(string name) {
        if (name == JwtBearerDefaults.AuthenticationScheme)
            return base.GetSchemeAsync(InterceptedScheme);
        return base.GetSchemeAsync(name);
    }
}