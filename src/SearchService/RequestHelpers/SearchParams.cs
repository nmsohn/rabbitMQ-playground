namespace SearchService.RequestHelpers;

public record SearchParams
{
    public string? Keyword { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 4;
    public string? Seller { get; init; } = string.Empty;
    public string? Winner { get; init; } = string.Empty;
    public string? OrderBy { get; init; } = string.Empty;
    public string? FilterBy { get; init; } = string.Empty;
}