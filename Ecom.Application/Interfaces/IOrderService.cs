using Ecom.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto dto, string userId);
        Task<List<OrderDto>> GetMyOrdersAsync(string userId);
    }
}
