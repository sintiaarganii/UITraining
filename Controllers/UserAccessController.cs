using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class UserAccessController : Controller
    {
        private readonly IUserAccess _users;
        private readonly ApplicationContext _context;
        public UserAccessController( IUserAccess users, ApplicationContext context)
        {
            _context = context;
            _users = users;
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            var data = _users.GetlistUser();
            return View(data);
        }

        public IActionResult Edit(int id)
        {
            var data = _users.GetUserById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(UserAccessDTO userAccessDTO)
        {
            var data = _users.EditUser(userAccessDTO);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var data = _users.DeleteUser(id);
            if (data)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Cannot Deleted this User.");
        }

        [HttpPost]
        public IActionResult Login(UserAccessDTO loginDTO)
        {
            try
            {
                var datauser = _users.Login(loginDTO.Username, loginDTO.Password);
                if (datauser)
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                TempData["ErrorMessage"] = "Username or Password is incorrect!";
                return View(loginDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while logging in.";
                return View(loginDTO);
            }
        }

        [HttpPost]
        public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        {
            if (userAccessDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password min 7 character";
                return View(userAccessDTO);
            }

            if (userAccessDTO.Password != userAccessDTO.MatchPassword)
            {
                TempData["ErrorMessage"] = "Password and Confirm Password must be the same!";
                return View(userAccessDTO);
            }

            var data = _context.UserAccesses
                .FirstOrDefault(x => x.Username == userAccessDTO.Username);
            if (data != null)
            {
                TempData["ErrorMessage"] = "Username is already in use. Please choose another username.";
                return View(userAccessDTO);
            }

            try
            {
                var datauser = _users.AddUser(userAccessDTO);
                if (datauser)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction("Login", "UserAccess"); 
                }

                TempData["ErrorMessage"] = "Failed to register user. Please try again.";
                return View(userAccessDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while registering the user.";
                return View(userAccessDTO);
            }
        }
    }
}
