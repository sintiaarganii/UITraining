using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DB
{
    public class UserAccess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string salt { get; set; }
        public string pwd_hash { get; set; }
        public DateTime AccessDate { get; set; }

        public GeneralStatusData UsersStatus { get; set; }

    }
}
