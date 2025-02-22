using AuctionService.Data;
using AuctionService.Dtos;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext auctionDbContext, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions([FromQuery] string date)
    {
        var query = auctionDbContext.Auctions.OrderBy(x => x.Item!.Make).AsQueryable();

        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) >= 0);
        }

        return await query.ProjectTo<AuctionDto>(mapper.ConfigurationProvider).ToListAsync();
        
        // var auctions = await auctionDbContext.Auctions
        //     .Include(x => x.Item)
        //     .OrderBy(x => x.Item!.Make)
        //     .ToListAsync();
        //
        // return mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById([FromRoute] Guid id)
    {
        //Find vs FirstOrDeafult
        var auction = await auctionDbContext.Auctions
            .Include(x => x.Item) // eager fetch is recommended (avoid n+1)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound();
        }
        
        return mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction([FromBody] CreateAuctionDto createAuctionDto)
    {
        var auction = mapper.Map<Auction>(createAuctionDto);
        //TODO: Add current user as a seller
        auction.Seller = "test";
        
        auctionDbContext.Auctions.Add(auction); //memory
        
        var result = await auctionDbContext.SaveChangesAsync() > 0;

        if (!result)
        {
            return BadRequest("Could not create auction in DB");
        }
        
        return CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction([FromRoute] Guid id, [FromBody] UpdateAuctionDto updateAuctionDto)
    {
        //TODO: check seller
        
        var auction = await auctionDbContext.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null)
        {
            return NotFound();
        }
        
        var updated = mapper.Map(updateAuctionDto, auction); //how does it handle guid?
        auction = updated;
        
        var result = await auctionDbContext.SaveChangesAsync() > 0;
        if (!result)
        {
            return BadRequest("Could not update auction in DB");
        }
        return NoContent(); //TODO: modify postman script
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction([FromRoute] Guid id)
    {
        //TODO: check seller

        var auction = await auctionDbContext.Auctions.FindAsync(id);
        if (auction == null)
        {
            return NotFound();
        }
        
        auctionDbContext.Auctions.Remove(auction);
        var result = await auctionDbContext.SaveChangesAsync() > 0;
        if (!result)
        {
            return BadRequest("Could not delete auction in DB");
        }
        return NoContent();
    }
}