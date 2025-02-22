using MongoDB.Entities;

namespace SearchService.Models;

public class Item : Entity
{
    public string? Make  { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }
    public int Mileage { get; set; }
    public string? ImageUrl { get; set; }
    public decimal ReservePrice { get; init; }
    public string? Seller { get; init; }
    public string? Winner { get; init; }
    public decimal? SoldPrice { get; init; }
    public decimal? CurrentHighBid { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public DateTime? AuctionEnd { get; init; }
}