using ClothMarket.DAL.Interfaces;
using ClothMarket.Domain.Entity;
using ClothMarket.Domain.ViewModels.Product;
using ClothMarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {

            var response = _productService.GetProducts();
            if (response.StatusCode==Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetProduct", response.Data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error", $"{response.Description}");
        }
        [HttpGet]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView();
            }

            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _productService.Create(model, imageData);
                }
                else
                {
                    await _productService.Edit(model.Id, model);
                }
                return RedirectToAction("GetProducts");
            }
            return View();

            
        }
        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _productService.GetTypes();
            return Json(types.Data);
        }


    }
}
