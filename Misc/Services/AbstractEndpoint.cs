using InnoClinic.Shared.Misc.Services.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using HttpMethod = InnoClinic.Shared.Domain.Models.HttpMethod;

namespace InnoClinic.Shared.Misc.Services;

public abstract class AbstractEndpoint : IEndpoint {
    public abstract string Pattern { get; }

    public abstract HttpMethod Method { get; }

    protected abstract Delegate EndpointHandler { get; }

    public void MapEndpoint(IEndpointRouteBuilder app) {
        switch (Method) {
            case HttpMethod.Get:
                app.MapGet(Pattern, EndpointHandler);
                break;

            case HttpMethod.Post:
                app.MapPost(Pattern, EndpointHandler);
                break;

            case HttpMethod.Put:
                app.MapPut(Pattern, EndpointHandler);
                break;

            case HttpMethod.Patch:
                app.MapPatch(Pattern, EndpointHandler);
                break;

            case HttpMethod.Delete:
                app.MapDelete(Pattern, EndpointHandler);
                break;
        }
    }
}
