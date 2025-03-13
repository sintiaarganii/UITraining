using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using UITraining.Interfaces;
using Microsoft.EntityFrameworkCore;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class UserAccessServices : IUserAccess
    {
        private readonly ApplicationContext _context;

        public UserAccessServices(ApplicationContext context)
        {
            _context = context;
        }

        public bool AddUser(UserAccessDTO users)
        {
            var user = new UserAccess
            {
                Name = users.Name,
                Username = users.Username,
                Password = users.Password,
                AccessDate = DateTime.Now,
                UsersStatus = GeneralStatus.GeneralStatusData.published
            };

            _context.UserAccesses.Add(user);
            _context.SaveChanges();
            return true;
        }


        public bool ValidateLogin(string username, string password)
        {
            var user = _context.UserAccesses
                .FirstOrDefault(x => x.Username == username && x.Password == password && x.UsersStatus != GeneralStatusData.delete);
            if (user != null)
            {
                user.AccessDate = DateTime.Now;
                _context.SaveChanges();
                return true;
            }

            return false;
        }



        //public List<UserAccessDTO> GetAllUser()
        //{
        //    var user = _context.UserAccesses.
        //        Where(x => x.UserStatus != GeneralStatusData.deleted)
        //        .Select(x => new UserAccessDTO
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Username = x.Username,
        //            Password = x.Password,
        //            MatchPassword = x.Password,
        //            StatusUser = x.UserStatus

        //        }).ToList();
        //    return user;
        //}
        //public UserAccess GetById(int id)
        //{
        //    var users = _context.UserAccesses.Where(x => x.Id == id && x.UserStatus != GeneralStatusData.deleted).FirstOrDefault();

        //    if (users == null)
        //    {
        //        return new UserAccess();
        //    }

        //    return users;
        //}
        //public bool UpdateUsers(UserAccessDTO userAccessDTO)
        //{
        //    var data = _context.UserAccesses.FirstOrDefault(x => x.Id == userAccessDTO.Id);
        //    if (data == null)
        //    {
        //        return false;
        //    }

        //    data.UserStatus = userAccessDTO.StatusUser;
        //    data.Name = userAccessDTO.Name;

        //    _context.UserAccesses.Update(data);
        //    _context.SaveChanges();

        //    return true;
        //}

        //public bool DeleteUser(int Id)
        //{
        //    var data = _context.UserAccesses.FirstOrDefault(x => x.Id == Id);
        //    if (data != null && data.UserStatus != GeneralStatusData.deleted)
        //    {
        //        data.UserStatus = GeneralStatusData.deleted;
        //        _context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public List<UserAccessDTO> GetlistUser()
        {
            var data = _context.UserAccesses.Where(x => x.UsersStatus != GeneralStatusData.deleted).Select(x => new UserAccessDTO
            {
                Id = x.Id,
                Name = x.Name,
                Username = x.Username,
                Password = x.Password,
                MatchPassword = x.Password,
                UsersStatus = x.UsersStatus

            }).ToList();
            return data;

        }

        public UserAccess GetUserById(int id)
        {
            var data = _context.UserAccesses.Where(x => x.Id == id && x.UsersStatus != GeneralStatusData.deleted).FirstOrDefault();
            if (data == null)
            {
                return new UserAccess();
            }

            return data;
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
            //_context.Products.Update(data);
            _context.SaveChanges();
            return true;
        }
    }
}
