namespace UITraining.Models.DB
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public ProductStatus Status { get; set; }
    }

    public enum ProductStatus
    {
        published, //bisa dilihat semua
        unpublihsed, //hanya admin yg bisa liat
        deleted //hanya bisa diliat di database
    }
}
