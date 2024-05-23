using Microsoft.AspNetCore.Routing;

namespace InnoClinic.Shared.Misc.Services.Abstraction;

public interface IEndpoint {
    void MapEndpoint(IEndpointRouteBuilder app);
}