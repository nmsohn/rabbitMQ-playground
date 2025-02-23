using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionCreatedConsumer(IMapper mapper) : IConsumer<AuctionCreated>
{
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine($"consuming auction created: {context.Message.Id}");
        var item = mapper.Map<Item>(context.Message);
        
        if(item.Model == "Foo") throw new ArgumentException("Cannot create item with model Foo");
        
        await item.SaveAsync();
    }
}