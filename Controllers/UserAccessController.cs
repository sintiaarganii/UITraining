using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UITraining.Interfaces;
using UITraining.Models.DB;
using UITraining.Models.DTO;

namespace UITraining.Controllers
{
    public class UserAccessController : Controller
    {
        private readonly IUserAccess _users;
        public UserAccessController( IUserAccess users)
        {
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
            return BadRequest("Gagal menghapus User.");
        }
    }
}
