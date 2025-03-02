using UITraining.Models.DB;
using UITraining.Models.DTO;
namespace UITraining.Interfaces
{
    public interface IProduct
    {
        List<ProductDTO> GetAllProducts();

        public Product GetProductById(int id);

        public bool EditProduct(ProductDTO product);
        public bool DeletedProduct(int id);
        public bool AddProduct(ProductDTO product);

    }
}
