using System.ComponentModel.DataAnnotations;
using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Domain.Models;

public record PageDesc : IPageDesc {
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; init; }

    [Required, Range(1, 128)]
    public int PageSize { get; init; }
}
