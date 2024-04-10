using InnoClinic.Shared.Domain.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Shared.Misc;

public static class Extensions {
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPageDesc pageDesc) {
        var skip = (pageDesc.PageNumber - 1) * pageDesc.PageSize;
        var take = pageDesc.PageSize;
        return query
            .Skip(skip)
            .Take(take);
    }

    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, IPageDesc pageDesc) {
        var skip = (pageDesc.PageNumber - 1) * pageDesc.PageSize;
        var take = pageDesc.PageSize;
        return collection
            .Skip(skip)
            .Take(take);
    }

    public static string GetOrThrow<TConfiguration>(this TConfiguration config, string key)
    where TConfiguration : IConfiguration {
        var val = config[key];
        if (val is null) {
            throw new ArgumentException($"Config does not contain {key}");
        }
        return val;
    }

}
