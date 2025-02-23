using System.ComponentModel.DataAnnotations;

namespace AuctionService.Dtos;

public record ItemDto
{
    [Required]
    public required string? Make  { get; init; }
    [Required]
    public required string? Model { get; init; }
    [Required]
    public required int Year { get; init; }
    [Required]
    public required string? Color { get; init; }
    [Required]
    public required int Mileage { get; init; }
    [Required]
    public required string? ImageUrl { get; init; }
}