using System.ComponentModel.DataAnnotations;

namespace AuctionService.Dtos;

public record CreateAuctionDto
{
    [Required] public ItemDto? Item { get; init; }
    public int ReservePrice { get; init; }
    public DateTime AuctionEnd { get; init; }
}