using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using System.Linq;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class SupplierServices : ISupplier
    {
        private readonly ApplicationContext _context;

        public SupplierServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<SupplierDTO> GetlistSupplier()
        {
            var dataSupp = _context.Suppliers.Where(x => x.StatusData != GeneralStatusData.deleted).Select(x => new SupplierDTO
            {
                Id = x.Id,
                SupplierName = x.SupplierName,
                SupplierAddress = x.SupplierAddress,
                StatusData = x.StatusData,

            }).ToList();
            return dataSupp;

        }
        public List<SelectListItem> Suppliers()
        {
            var datas = _context.Suppliers.Where(x => x.StatusData == GeneralStatusData.active)
                .Select(x => new SelectListItem
                {
                    Text = x.SupplierName,
                    Value = x.Id.ToString()
                }).ToList();
            return datas;
        }

        public Supplier GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.Where(x => x.Id == id && x.StatusData != GeneralStatusData.deleted).FirstOrDefault();

            if (supplier == null)
            {
                return new Supplier();
            }

            return supplier;
        }

        public bool AddSupplier(SupplierDTO supplierDTO)
        {
            var supplier = new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                SupplierAddress = supplierDTO.SupplierAddress,
                StatusData = supplierDTO.StatusData
            };

            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return true;
        }

        public bool EditSupplier(SupplierDTO supplier)
        {
            var dataSupp = _context.Suppliers.FirstOrDefault(x => x.Id == supplier.Id);
            if (dataSupp == null)
            {
                return false; 
            }

            dataSupp.SupplierName = supplier.SupplierName;
            dataSupp.SupplierAddress = supplier.SupplierAddress;
            dataSupp.StatusData = supplier.StatusData;

            _context.Suppliers.Update(dataSupp);
            _context.SaveChanges();

            return true; 
        }
        public bool DeleteSupplier(int supplierId)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == supplierId);
            if (supplier != null && supplier.StatusData != GeneralStatusData.deleted)
            {
                supplier.StatusData = GeneralStatusData.deleted;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
