using System.Security.Claims;
using InnoClinic.Shared.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Shared.Misc;

namespace InnoClinic.Shared.Misc.Auth;

public class ClaimUserDescriptorFactory(IConfiguration config) {
    public IUserDescriptor CreateFrom(ClaimsPrincipal user) {
        var id = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var name = user.Claims.First(x => x.Type == "name").Value;
        
        var adminRoleName = config.GetOrThrow("AdminRoleName");
        var IsAdmin = user.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == adminRoleName);

        return new ClaimUserDescriptor() {
            Id = id,
            Name = name,
            IsAdminBool = IsAdmin,
        };
    }
}