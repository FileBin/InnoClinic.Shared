namespace InnoClinic.Shared.Domain.Abstractions;

public interface IPageDesc {

    int PageNumber { get; }
    int PageSize { get; }
}