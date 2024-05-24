using InnoClinic.Shared.Domain.Models;
using InnoClinic.Shared.Misc.Services.Abstraction;
using Microsoft.AspNetCore.Routing;

namespace InnoClinic.Shared.Misc.Services;

public abstract class AbstractEndpoint : IEndpoint {
    public abstract string Pattern { get; }

    public abstract EndpointHttpMethods Method { get; }

    protected abstract Delegate EndpointHandler { get; }

    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapEndpoint(Method, [Pattern], EndpointHandler);
    }
}


public abstract class MultipleEndpoint : IEndpoint {
    public abstract IEnumerable<string> Patterns { get; }

    public abstract EndpointHttpMethods Method { get; }

    protected abstract Delegate EndpointHandler { get; }

    public void MapEndpoint(IEndpointRouteBuilder app) {
        app.MapEndpoint(Method, Patterns, EndpointHandler);
    }
}
