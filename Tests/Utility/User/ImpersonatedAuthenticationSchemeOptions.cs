namespace InnoClinic.Shared.Tests.Utility.User;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class ImpersonatedAuthenticationSchemeOptions : AuthenticationSchemeOptions {
    public string OriginalScheme { get; set; } = "";
    public Action<List<Claim>> Configure { get; set; } = (_) => { };
}
