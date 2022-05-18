using ClothMarket.Domain.Entity;
using ClothMarket.Domain.Response;
using ClothMarket.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.Service.Interfaces
{
    public interface IProductService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        IBaseResponse<List<Product>> GetProducts();
        Task<IBaseResponse<ProductViewModel>> GetProduct(int id);
        Task<IBaseResponse<Dictionary<int, string>>> GetProduct(string term);
        Task<IBaseResponse<Product>> Create(ProductViewModel product, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteProduct(int id);
        Task<IBaseResponse<Product>> Edit(int id, ProductViewModel model);
    }
}
