using AuctionService.Dtos;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item).ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(dest => dest.Item, 
                opt => opt.MapFrom(src => src.Item));
        CreateMap<AuctionDto, AuctionCreated>();
        CreateMap<Item, AuctionUpdated>();
    }
}