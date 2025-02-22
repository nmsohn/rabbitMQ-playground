using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public abstract class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("SearchDb")));
        
        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();
        
        var count = await DB.CountAsync<Item>();
        
        using var scope = app.Services.CreateScope();
        
        var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();
        var items = await httpClient.GetItemsForSearchDb();
        
        Console.WriteLine($"Found {items.Count} items");

        if (items.Count > 0)
        {
            await DB.SaveAsync(items);
        }

        // if (count == 0)
        // {
        //     Console.WriteLine("No data - attempting to initialize database");
        //     var itemData = await File.ReadAllTextAsync("Data/auctions.json");
        //     
        //     var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        //     
        //     var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
        //
        //     if (items != null) await DB.SaveAsync(items);
        // }
    }
}