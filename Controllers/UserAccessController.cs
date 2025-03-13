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










        [HttpPost]
        public IActionResult Login(UserAccessDTO loginDTO)
        {
            try
            {
                var datauser = _users.ValidateLogin(loginDTO.Username, loginDTO.Password);
                if (datauser)
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                TempData["ErrorMessage"] = "Username atau Password salah!";
                return View(loginDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Terjadi kesalahan saat login.";
                return View(loginDTO);
            }
        }

        [HttpPost]
        public IActionResult RegisterUser(UserAccessDTO userAccessDTO)
        {
            if (userAccessDTO.Password.Length < 7)
            {
                TempData["ErrorMessage"] = "Password minimal 7 karakter";
                return View(userAccessDTO);
            }

            if (userAccessDTO.Password != userAccessDTO.MatchPassword)
            {
                TempData["ErrorMessage"] = "Password dan Konfirmasi Password harus sama!";
                return View(userAccessDTO);
            }

            var data = _context.UserAccesses
                .FirstOrDefault(x => x.Username == userAccessDTO.Username);
            if (data != null)
            {
                TempData["ErrorMessage"] = "Username sudah digunakan. Silakan pilih username lain.";
                return View(userAccessDTO);
            }

            try
            {
                var datauser = _users.AddUser(userAccessDTO);
                if (datauser)
                {
                    TempData["SuccessMessage"] = "Registrasi berhasil! Silakan login."; // Pesan sukses
                    return RedirectToAction("Login", "UserAccess"); // Redirect jika berhasil
                }

                TempData["ErrorMessage"] = "Gagal mendaftarkan user. Silakan coba lagi.";
                return View(userAccessDTO);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Terjadi kesalahan saat mendaftarkan user.";
                return View(userAccessDTO);
            }
        }










    }
}
