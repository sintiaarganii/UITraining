using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Interfaces
{
    public interface IUserAccess
    {
        public bool AddUser(UserAccessDTO users);
        public List<UserAccessDTO> GetlistUser();
        public UserAccess GetUserById(int id);
        public bool EditUser(UserAccessDTO userAccessDTO);
        public bool DeleteUser(int id);
    }
}
