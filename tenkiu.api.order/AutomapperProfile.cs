using AutoMapper;
using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Entities;
using vm.common.Utils;

namespace tenkiu.api.order;

public class AutomapperProfile : Profile
{
  public AutomapperProfile()
  {
    this.MapSellOrderToDtos();
    this.MapSellOrderDetailsToDtos();
    this.MapBuyOrderToDtos();
    this.MapBuyOrderDetailsToDtos();
  }

  private void MapBuyOrderToDtos()
  {
    this.CreateMap<BuyOrder, BaseBuyOrderDto>()
        .IgnoreAllNonExisting()
        .ReverseMap();

    this.CreateMap<BuyOrder, CreateBuyOrderDto>()
        .IncludeBase<BuyOrder, BaseBuyOrderDto>()
        .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.BuyOrderDetails))
        .ReverseMap();

    this.CreateMap<BuyOrder, UpdateBuyOrderDto>()
        .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.BuyOrderDetails))
        .IncludeBase<BuyOrder, BaseBuyOrderDto>();

    this.CreateMap<BuyOrder, ResponseBuyOrderDto>()
        .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.BuyOrderDetails))
        .IncludeBase<BuyOrder, BaseBuyOrderDto>();
  }

  private void MapBuyOrderDetailsToDtos()
  {
    this.CreateMap<BuyOrderDetail, BaseBuyOrderDetailDto>()
        .IgnoreAllNonExisting()
        .ReverseMap();

    this.CreateMap<BuyOrderDetail, CreateBuyOrderDetailDto>()
        .IncludeBase<BuyOrderDetail, BaseBuyOrderDetailDto>()
        .ReverseMap();

    this.CreateMap<BuyOrderDetail, UpdateBuyOrderDetailDto>()
        .IncludeBase<BuyOrderDetail, BaseBuyOrderDetailDto>();

    this.CreateMap<BuyOrderDetail, ResponseBuyOrderDetailDto>()
        .IncludeBase<BuyOrderDetail, BaseBuyOrderDetailDto>();
  }

  private void MapSellOrderToDtos()
  {
    this.CreateMap<SellOrder, BaseSellOrderDto>()
      .IgnoreAllNonExisting()
      .ReverseMap();

    this.CreateMap<SellOrder, CreateSellOrderDto>()
      .IncludeBase<SellOrder, BaseSellOrderDto>()
      .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.SellOrderDetails))
      .ReverseMap();

    this.CreateMap<SellOrder, UpdateSellOrderDto>()
      .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.SellOrderDetails))
      .IncludeBase<SellOrder, BaseSellOrderDto>();

    this.CreateMap<SellOrder, ResponseSellOrderDto>()
      .ForMember(v => v.OrderDetails, opt => opt.MapFrom(v => v.SellOrderDetails))
      .IncludeBase<SellOrder, BaseSellOrderDto>();
  }

  private void MapSellOrderDetailsToDtos()
  {
    this.CreateMap<SellOrderDetail, BaseSellOrderDetailDto>()
      .IgnoreAllNonExisting()
      .ReverseMap();

    this.CreateMap<SellOrderDetail, CreateSellOrderDetailDto>()
      .IncludeBase<SellOrderDetail, BaseSellOrderDetailDto>()
      .ReverseMap();

    this.CreateMap<SellOrderDetail, UpdateSellOrderDetailDto>()
      .IncludeBase<SellOrderDetail, BaseSellOrderDetailDto>();

    this.CreateMap<SellOrderDetail, ResponseSellOrderDetailDto>()
      .IncludeBase<SellOrderDetail, BaseSellOrderDetailDto>();
  }
}
