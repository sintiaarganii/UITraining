using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;

namespace UITraining.Services
{
    public class ProductServices : IProduct
    {
        private ApplicationContext _context;
        public ProductServices(ApplicationContext context) 
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            var products = _context.Products.Where(x => x.Status != ProductStatus.deleted).ToList();
            return products;
        }
    }
}
