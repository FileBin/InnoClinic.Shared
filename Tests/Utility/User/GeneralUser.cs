namespace InnoClinic.Shared.Tests.Utility.User;

using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class GeneralUser : ImpersonatedUser {
    public GeneralUser(IOptionsMonitor<ImpersonatedAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) { }
}
