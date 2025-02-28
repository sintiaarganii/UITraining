using UITraining.Models.DB;

namespace UITraining.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAllProducts();

        public Product GetProductById(int id);

        public bool EditProduct(Product product);
        public bool DeletedProduct(int id); 

    }
}
