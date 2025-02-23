namespace Contracts;

public record AuctionCreated
{
    public string Id { get; set; }
    public decimal ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public decimal SoldPrice { get; set; }
    public decimal CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; } 
    public string Make  { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }
}