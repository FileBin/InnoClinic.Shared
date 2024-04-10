using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Misc.Auth;

internal class ClaimUserDescriptor : IUserDescriptor {

    public required string Id { get; init; }
    public required string Name { get; init; }
    internal required bool IsAdminBool { get; init; }

    public bool IsAdmin() => IsAdminBool;
}
