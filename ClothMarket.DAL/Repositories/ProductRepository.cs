using ClothMarket.DAL.Interfaces;
using ClothMarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product entity)
        {
            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
            
        }

        public async Task Delete(Product entity)
        {

            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Product> GetAll()
        {
            return _context.Product;
        }

        public async Task<Product> Update(Product entity)
        {
            _context.Product.Update(entity);
            await _context.SaveChangesAsync();
            //???
            return entity;
        }
    }
}
