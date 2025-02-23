using System.Globalization;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionServiceHttpClient(HttpClient httpClient, IConfiguration config)
{
    //Note: what if there is a large amount of data to be fetched?
    public async Task<List<Item>> GetItemsForSearchDb()
    {
        //TODO: a way to avoid hard coded url?
        var url = config["AuctionServiceUrl"] + "/api/auctions" + "?date=";
        
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(a => a.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();
        
        if (string.IsNullOrEmpty(lastUpdated))
        {
            Console.WriteLine("Last updated is empty: " + lastUpdated);
        }
        else
        {
            Console.WriteLine("Last updated is not empty: " + lastUpdated);
        }
        
        // if (!string.IsNullOrEmpty(lastUpdated))
        // {
        //     url += "?date=" + lastUpdated;
        // }
        
        url += lastUpdated;
        var result= await httpClient.GetFromJsonAsync<List<Item>>(url) ?? throw new InvalidOperationException();
        return result;
    }
}