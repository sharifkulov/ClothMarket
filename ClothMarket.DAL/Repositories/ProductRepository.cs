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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Product entity)
        {
            await _context.Product.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {

            _context.Product.Remove(entity);
            await _context.SaveChangesAsync();
            //???
            return true;
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetByName(string name)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Product>> Select()
        {
            return await _context.Product.ToListAsync();
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
