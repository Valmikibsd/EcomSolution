using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Application.Services
{
    public class OrderService: IOrderService
    {
        public readonly ApplicationDbContext _context;
        public readonly Mapper _mapper;
        public OrderService(ApplicationDbContext context, Mapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(CreateOrderDto dto, string userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderNo = GenerateOrderNo(),
                ShippingAddress = dto.ShippingAddress
            };

            foreach (var item in dto.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            order.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);

            await  _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetMyOrdersAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    OrderNo = o.OrderNo,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    ShippingAddress = o.ShippingAddress,
                    Items = o.Items.Select(i => new OrderItemDto
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        private static string GenerateOrderNo()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }

        //public async Task Cre

    }
}
