using static UITraining.Models.GeneralStatus;

namespace UITraining.Models.DTO
{
    public class UserAccessDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MatchPassword { get; set; }
        public GeneralStatusData UsersStatus { get; set; }
    }
}
