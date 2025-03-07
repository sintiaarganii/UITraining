using static UITraining.Models.GeneralStatus;
using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }

        public ICollection<Product> products { get; set; } = new List<Product>();
        public GeneralStatusData StatusData { get; set; }
    }
}
