using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface ISupplier
    {
        public List<SupplierDTO> GetlistSupplier();
        public List<SelectListItem> Suppliers();
        public bool AddSupplier(SupplierDTO supplier);
        public Supplier GetSupplierById(int id);
        public bool EditSupplier(SupplierDTO supplier);
        public bool DeleteSupplier(int supplierId);

    }
}
