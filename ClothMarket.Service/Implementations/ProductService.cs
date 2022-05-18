using ClothMarket.DAL.Interfaces;
using ClothMarket.Domain.Entity;
using ClothMarket.Domain.Enum;
using ClothMarket.Domain.Extensions;
using ClothMarket.Domain.Response;
using ClothMarket.Domain.ViewModels.Product;
using ClothMarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothMarket.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            
            _productRepository = productRepository;
        }
        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((Category[])Enum.GetValues(typeof(Category)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<ProductViewModel>> GetProduct(int id)
        {
            
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id); ;
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ProductViewModel()
                {
                    DateCreate = product.DateCreate.ToLongDateString(),
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category.GetDisplayName(),
                    Size = product.Size,
                    Image = product.Avatar,
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Dictionary<int, string>>> GetProduct(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var product = await _productRepository.GetAll()
                .Select(x => new ProductViewModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Description = x.Description,
                     Size = x.Size,
                     DateCreate = x.DateCreate.ToLongDateString(),
                     Price = x.Price,
                     Category = x.Category.GetDisplayName()
                 })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = product;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Product>> Edit(int id, ProductViewModel model)
        {
            
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }


                product.Description = model.Description;
                product.Size = model.Size;
                product.Price = model.Price;
                product.DateCreate = DateTime.ParseExact(model.DateCreate, "yyyyMMdd HH:mm", null);
                product.Name = model.Name;

                await _productRepository.Update(product);


                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK,
                };
                // Category

            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[EditProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<Product>> Create(ProductViewModel model, byte[] imageData)
        {
            
            try
            {
                var product = new Product()
                {
                    Description = model.Description,
                    DateCreate = DateTime.Now,
                    Size = model.Size,
                    Price = model.Price,
                    Name = model.Name,
                    Category = (Category)Convert.ToInt32(model.Category),
                    Avatar = imageData
                };

                await _productRepository.Create(product);
                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[CreateCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
        }
        public async Task<IBaseResponse<bool>> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id); ;
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                //await _productRepository.Delete(product);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Product>> GetProducts()
        {
            
            try
            {
                var products = _productRepository.GetAll().ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
