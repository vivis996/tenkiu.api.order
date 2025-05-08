using AutoMapper;
using tenkiu.api.order.Models.Dto.Order;
using tenkiu.api.order.Models.Dto.OrderDetail;
using tenkiu.api.order.Models.Entities;
using vm.common.Utils;

namespace tenkiu.api.order;

public class AutomapperProfile : Profile
{
  public AutomapperProfile()
  {
    this.MapOrderToDtos();
    this.MapOrderDetailsToDtos();
  }

  private void MapOrderToDtos()
  {
    this.CreateMap<Order, BaseOrderDto>()
      .IgnoreAllNonExisting()
      .ReverseMap();

    this.CreateMap<Order, CreateOrderDto>()
      .IncludeBase<Order, BaseOrderDto>()
      .ReverseMap();

    this.CreateMap<Order, UpdateOrderDto>()
      .IncludeBase<Order, BaseOrderDto>();

    this.CreateMap<Order, ResponseOrderDto>()
      .IncludeBase<Order, BaseOrderDto>();
  }

  private void MapOrderDetailsToDtos()
  {
    this.CreateMap<OrderDetail, BaseOrderDetailDto>()
      .IgnoreAllNonExisting()
      .ReverseMap();

    this.CreateMap<OrderDetail, CreateOrderDetailDto>()
      .IncludeBase<OrderDetail, BaseOrderDetailDto>()
      .ReverseMap();

    this.CreateMap<OrderDetail, UpdateOrderDetailDto>()
      .IncludeBase<OrderDetail, BaseOrderDetailDto>();

    this.CreateMap<OrderDetail, ResponseOrderDetailDto>()
      .IncludeBase<OrderDetail, BaseOrderDetailDto>();
  }
}
