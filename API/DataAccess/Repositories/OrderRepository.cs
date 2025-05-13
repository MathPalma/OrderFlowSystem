﻿using API.DataAccess.DbContexts;
using API.Domain.Entities;
using API.Domain.Repositories;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrdersByFilters(OrderFilterViewModel filters)
        {
            var query = _context.Orders
                .Include(o => o.Items)
                .AsQueryable();

            if (filters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == filters.CustomerId);
            }

            if (filters.Status.HasValue)
            {
                query = query.Where(o => o.Status == filters.Status);
            }

            if (filters.StartDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt >= filters.StartDate);
            }

            if (filters.EndDate.HasValue)
            {
                query = query.Where(o => o.CreatedAt <= filters.EndDate);
            }

            return await query.OrderByDescending(o => o.CreatedAt).ToListAsync();
        }

        public async Task CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
