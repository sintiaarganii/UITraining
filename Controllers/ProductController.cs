using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Services;

namespace UITraining.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _interface;

        public ProductController(IProduct interfaces)
        {
            _interface = interfaces;
        }

        public IActionResult Index()
        {
            var products = _interface.GetAllProducts();
            return View(products);
        }

        public IActionResult Edit(int Id)
        {
            var product = _interface.GetProductById(Id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var EditProduct = _interface.EditProduct(product);
            if (EditProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var deleteProduct = _interface.DeletedProduct(Id);
            if (deleteProduct)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this product");
        }

    }
}
