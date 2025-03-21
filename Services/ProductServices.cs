using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(y=>y.supplier)
                .Where(x=>x.Status != GeneralStatusData.deleted)
                .Select(x=> new ProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stock = x.Stock,
                    Status = x.Status,
                    SupplierName = x.supplier.SupplierName,
                }).ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id && x.Status != GeneralStatusData.deleted).FirstOrDefault();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }
        public bool AddProduct(ProductDTO product)
        {
            var data = new Product();
            data.Name = product.Name;
            data.Description = product.Description;
            data.Stock = product.Stock;
            data.Price = product.Price;
            data.Status = product.Status;
            data.IdSupplier = product.IdSupplier;

            _context.Add(data);
            _context.SaveChanges();
            return true;
        }
        public bool EditProduct(ProductDTO product)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }

            data.Name = product.Name;
            data.Stock = product.Stock;
            data.Description = product.Description;
            data.Price = product.Price;
            data.Status = product.Status;

            _context.Products.Update(data);
            _context.SaveChanges();
            return true;
        }

        public bool DeletedProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null && product.Status != GeneralStatusData.deleted)
            {
                product.Status = GeneralStatusData.deleted; 
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
