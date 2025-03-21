using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Interfaces;
using static UITraining.Models.GeneralStatus;
using UITraining.Helper;

namespace UITraining.Services
{
    public class UserAccessServices : IUserAccess
    {
        private readonly ApplicationContext _context;
        private readonly string _pepper;
        private readonly string _interation;
        public UserAccessServices(ApplicationContext context, IConfiguration configuration)
        {
            _pepper = configuration.GetSection("Security:Pepper").Value ?? "";
            _interation = configuration.GetSection("Security:Interation").Value ?? "";
            _context = context;
        }

        public bool AddUser(UserAccessDTO users)
        {
            var UserExist = _context.UserAccesses.Where(x => x.Username == users.Username).FirstOrDefault();
            if (UserExist != null) return false;

            var generateSalt = Helper.Hasher.GenerateSalt();

            var user = new UserAccess
            {
                Name = users.Name,
                Username = users.Username,
                AccessDate = DateTime.Now,
                salt = generateSalt,
                UsersStatus = GeneralStatus.GeneralStatusData.published,
                pwd_hash = Hasher.ComputeHash(users.Password, generateSalt, _pepper, Convert.ToInt32(_interation))
            };
           
            _context.UserAccesses.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool Login(string username, string password)
        {
            var data = _context.UserAccesses.FirstOrDefault(x => x.Username == username && x.UsersStatus == GeneralStatus.GeneralStatusData.published);
            if (data == null) return false;

            var hashResult = Hasher.ComputeHash(password, data.salt, _pepper, Convert.ToInt32(_interation));

            return hashResult == data.pwd_hash;
        }

        public List<UserAccessDTO> GetlistUser()
        {
            var data = _context.UserAccesses.Where(x => x.UsersStatus != GeneralStatusData.deleted).Select(x => new UserAccessDTO
            {
                Id = x.Id,
                Name = x.Name,
                Username = x.Username,
                Password = "********", 
                MatchPassword = "********",
                UsersStatus = x.UsersStatus

            }).ToList();
            return data;
        }

        public UserAccess GetUserById(int id)
        {
            return _context.UserAccesses.FirstOrDefault(x => x.Id == id && x.UsersStatus != GeneralStatusData.deleted) ?? new UserAccess();
        }

        public bool EditUser(UserAccessDTO userAccessDTO)
        {
            var data = _context.UserAccesses.FirstOrDefault(x => x.Id == userAccessDTO.Id);
            if (data == null)
            {
                return false;
            }

            data.UsersStatus = userAccessDTO.UsersStatus;

            _context.UserAccesses.Update(data);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var data = _context.UserAccesses.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return false;
            }

            data.UsersStatus = GeneralStatusData.deleted;
            _context.SaveChanges();
            return true;
        }
    }
}
