using ClothMarket.DAL.Interfaces;
using ClothMarket.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ClothMarket.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return View();
        }
    }
}
