using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplier _supplier;
        public SupplierController(ISupplier supplier)
        {
            _supplier = supplier;
        }

        public IActionResult Index()
        {
            var supplier = _supplier.GetlistSupplier();
            return View(supplier);
        }

        public IActionResult EditSupplier(int id)
        {
            var supplier = _supplier.GetSupplierById(id);
            return View(supplier);
        }

        [HttpPost]
        public IActionResult EditSupplier(SupplierDTO supplier)
        {
            if (supplier.Id == 0)
            {
                var addSupplier = _supplier.AddSupplier(supplier);
                if (addSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                var editSupplier = _supplier.EditSupplier(supplier);
                if (editSupplier)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        public IActionResult Delete(int Id)
        {
            var deleteSupplier = _supplier.DeleteSupplier(Id);
            if (deleteSupplier)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this supplier");
        }
    }
}
