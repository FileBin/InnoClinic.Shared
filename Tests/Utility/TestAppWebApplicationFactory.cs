namespace InnoClinic.Shared.Tests.Utility;

using System.Security.Claims;
using InnoClinic.Shared.Tests.Utility.Auth;
using InnoClinic.Shared.Tests.Utility.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class {

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            // by default the OIDC auth scheme is intercepted and an auto-failing handler is returned,
            //  this ensures we don't accidentally call the discovery URL
            // CreateLoggedInClient will replace this provider with a different interceptor, last one in wins
            services.AddTransient<IAuthenticationSchemeProvider, AutoFailSchemeProvider>();
            services.AddAuthentication(AutoFailSchemeProvider.AutoFailScheme)
                .AddScheme<AutoFailOptions, AutoFail>(AutoFailSchemeProvider.AutoFailScheme, null);
        });
    }

    public HttpClient CreateLoggedInClient<T>(WebApplicationFactoryClientOptions options)
    where T : GeneralUser {
        return CreateLoggedInClient<T>(options, list => {
            list.Add(new Claim("scope", "testAPI"));
        });
    }

    /// <summary>
    /// This configures the "InterceptedScheme" to return a particular type of user, which we can also enrich with extra
    /// parameters/options for use in custom Claims
    /// </summary>
    /// <remarks>
    /// Adding a new user type:
    ///   1. Add a minimal implementation of ImpersonatedUser to be the user class (example below)
    ///   2. Add a new helper method with appropriate args typed to the new class (example above)
    /// </remarks>
    private HttpClient CreateLoggedInClient<T>(WebApplicationFactoryClientOptions options, Action<List<Claim>> configure)
        where T : ImpersonatedUser {
        var client = WithWebHostBuilder(
            builder => {
                builder.ConfigureTestServices(services => {
                    // configure the intercepting provider
                    services.AddTransient<IAuthenticationSchemeProvider, InterceptOidcAuthenticationSchemeProvider>();

                    // Add a "Test" scheme in to process the auth instead, using the provided user type
                    services.AddAuthentication(InterceptOidcAuthenticationSchemeProvider.InterceptedScheme)
                        .AddScheme<ImpersonatedAuthenticationSchemeOptions, T>("InterceptedScheme", options => {
                            options.OriginalScheme = "oidc";
                            options.Configure = configure;
                        });
                });
            })
            .CreateClient(options);

        return client;
    }



}
