using Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        var exception = context.Message.Exceptions.First();
        Console.WriteLine($"AuctionCreatedFaultConsumer: {exception.Message}");

        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "Foobar";
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine("Not an argument exception");
        }
    }
}