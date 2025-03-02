using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;

namespace UITraining.Services
{
    public class SupplierServices : ISupplier
    {
        private readonly ApplicationContext _context;

        public SupplierServices(ApplicationContext context)
        {
            _context = context;
        }

        //public List<Supplier> GetAllSupplier()
        //{
        //    var datas = _context.Suppliers.ToList();
        //    return datas;
        //}
        public List<SelectListItem> Suppliers()
        {
            var datas = _context.Suppliers
                .Select(x => new SelectListItem
                {
                    Text = x.SupplierName,
                    Value = x.Id.ToString()
                }).ToList();
            return datas;
        }
    }
}
