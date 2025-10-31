using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MicroServiceV2.Order.Application.Dtos;
using MicroServiceV2.Order.Domain.Entities;

namespace MicroServiceV2.Order.Application.Features.Orders
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
