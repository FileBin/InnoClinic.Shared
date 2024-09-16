using InnoClinic.Shared.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace InnoClinic.Shared.Misc;


public static class Helpers {
    private static readonly Dictionary<EndpointHttpMethods, Func<IEndpointRouteBuilder, string, Delegate, RouteHandlerBuilder>>
    _methodMapping =
    new() {
        { EndpointHttpMethods.Get, EndpointRouteBuilderExtensions.MapGet },
        { EndpointHttpMethods.Post, EndpointRouteBuilderExtensions.MapPost },
        { EndpointHttpMethods.Put, EndpointRouteBuilderExtensions.MapPut },
        { EndpointHttpMethods.Patch, EndpointRouteBuilderExtensions.MapPatch },
        { EndpointHttpMethods.Delete, EndpointRouteBuilderExtensions.MapDelete },
    };

    public static void MapEndpoint(this IEndpointRouteBuilder app, EndpointHttpMethods methods,
        IEnumerable<string> patterns, Delegate handler) {

        var pairs = _methodMapping
            .Where(pair => methods.HasFlag(pair.Key))
            .SelectMany(pair => patterns.Select(pattern => (pair.Value, pattern)));

        foreach (var (registrant, pattern) in pairs) {
            registrant.Invoke(app, pattern, handler);
        }
    }
}