namespace UITraining.Models.DB
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();   
    }
}
