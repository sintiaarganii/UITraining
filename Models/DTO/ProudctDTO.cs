using UITraining.Models.DB;

namespace UITraining.Models.DTO
{
    public class ProudctDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public bool IsAcive { get; set; }
        public ProductStatus Status { get; set; }
    }
}
