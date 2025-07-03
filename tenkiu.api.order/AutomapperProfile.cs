using AutoMapper;
using tenkiu.api.order.Models.Common;
using tenkiu.api.order.Models.Dto.BuyOrder;
using tenkiu.api.order.Models.Dto.BuyOrderDetail;
using tenkiu.api.order.Models.Dto.DeliveryPeriod;
using tenkiu.api.order.Models.Dto.SellOrder;
using tenkiu.api.order.Models.Dto.SellOrderDetail;
using tenkiu.api.order.Models.Dto.SellOrderPaymentHistory;
using tenkiu.api.order.Models.Entities;
using vm.common.Utils;

namespace tenkiu.api.order;

public class AutomapperProfile : Profile
{
  public AutomapperProfile()
  {
    this.MapSellOrderToDtos();
    this.MapSellOrderDetailsToDtos();
    this.MapSellOrderPaymentHistoryToDtos();
    this.MapBuyOrderToDtos();
    this.MapBuyOrderDetailsToDtos();
    this.MapDeliveryPeriodToDtos();
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
        .ForMember(v => v.TotalQuantity, opt => opt.MapFrom(v => v.BuyOrderDetails.Sum(o => o.Quantity)))
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
      .ForMember(v => v.TotalQuantity, opt => opt.MapFrom(v => v.SellOrderDetails.Sum(o => o.Quantity)))
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

  private void MapDeliveryPeriodToDtos()
  {
    this.CreateMap<DeliveryPeriod, BaseDeliveryPeriodDto>()
      .IgnoreAllNonExisting()
      .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new DateTimePeriod(
          src.StartDate.ToDateTime(TimeOnly.MinValue),
          src.EndDate.ToDateTime(TimeOnly.MinValue)
      )));

    this.CreateMap<DeliveryPeriod, CreateDeliveryPeriodDto>()
      .IncludeBase<DeliveryPeriod, BaseDeliveryPeriodDto>();

    this.CreateMap<DeliveryPeriod, UpdateDeliveryPeriodDto>()
      .IncludeBase<DeliveryPeriod, BaseDeliveryPeriodDto>();

    this.CreateMap<DeliveryPeriod, ResponseDeliveryPeriodDto>()
      .IncludeBase<DeliveryPeriod, BaseDeliveryPeriodDto>();
    
    //Inverse mapping for DeliveryPeriod
    this.CreateMap<BaseDeliveryPeriodDto, DeliveryPeriod>()
      .IgnoreAllNonExisting()
      .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Period.Start)))
      .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Period.End)));

    this.CreateMap<CreateDeliveryPeriodDto, DeliveryPeriod>()
      .IncludeBase<BaseDeliveryPeriodDto, DeliveryPeriod>();
    
    this.CreateMap<UpdateDeliveryPeriodDto, DeliveryPeriod>()
      .IncludeBase<BaseDeliveryPeriodDto, DeliveryPeriod>();
  }

  private void MapSellOrderPaymentHistoryToDtos()
  {
    this.CreateMap<SellOrderPaymentHistory, BaseSellOrderPaymentHistoryDto>()
        .IgnoreAllNonExisting();
    
    this.CreateMap<BaseSellOrderPaymentHistoryDto, SellOrderPaymentHistory>()
        .IgnoreAllNonExisting();

    this.CreateMap<CreateSellOrderPaymentHistoryDto, SellOrderPaymentHistory>()
        .IncludeBase<BaseSellOrderPaymentHistoryDto, SellOrderPaymentHistory>()
        .ForMember(dest => dest.IdClient, opt => opt.MapFrom(src => src.IdClient))
        .ForMember(dest => dest.SellOrderId, opt => opt.MapFrom(src => src.SellOrderId));

    this.CreateMap<SellOrderPaymentHistory, UpdateSellOrderPaymentHistoryDto>()
        .IncludeBase<SellOrderPaymentHistory, BaseSellOrderPaymentHistoryDto>();

    this.CreateMap<SellOrderPaymentHistory, ResponseSellOrderPaymentHistoryDto>()
        .IncludeBase<SellOrderPaymentHistory, BaseSellOrderPaymentHistoryDto>();
  }
}
