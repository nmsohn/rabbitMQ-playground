namespace AuctionService.Dtos;

public record AuctionDto
{
    public decimal ReservePrice { get; init; }
    public string? Seller { get; init; }
    public string? Winner { get; init; }
    public decimal? SoldPrice { get; init; }
    public decimal? CurrentHighBid { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime? AuctionEnd { get; init; }
    public required ItemDto Item { get; init; }
}