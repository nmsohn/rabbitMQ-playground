using System.Globalization;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionServiceHttpClient(HttpClient httpClient, IConfiguration config)
{
    //Note: what if there is a large amount of data to be fetched?
    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(a => a.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString(CultureInfo.InvariantCulture))
            .ExecuteFirstAsync();
        
        return await httpClient.GetFromJsonAsync<List<Item>>(config["AuctionServiceUrl"] + 
                                                             "/api/auctions?date=" + 
                                                             lastUpdated) ?? throw new InvalidOperationException();
    }
}