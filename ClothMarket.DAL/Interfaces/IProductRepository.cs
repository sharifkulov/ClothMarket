using ClothMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.DAL.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetByName(string name);
        
    }
}
