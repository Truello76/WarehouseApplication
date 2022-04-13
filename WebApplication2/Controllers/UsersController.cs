using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Areas.Identity.Data;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<WebApplication2User> _userManager;

        public UsersController(UserManager<WebApplication2User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchingName, string searchingUsername)
        {
            var userList = from x in _userManager.Users
                           select x;
            userList = userList.OrderBy(x => x.UserName);
            if (!String.IsNullOrEmpty(searchingName))
            {
                userList = userList.Where(x => x.FirstName.Contains(searchingName) || x.LastName.Contains(searchingName));
            }
            if (!String.IsNullOrEmpty(searchingUsername))
            {
                userList = userList.Where(x => x.UserName.Contains(searchingUsername));
            }
            return View(userList.ToList());
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        public async Task<IActionResult> EditPassword(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(string? id, WebApplication2User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            var x = await _userManager.FindByIdAsync(id);
            x.UserName = user.UserName;
            x.FirstName = user.FirstName;
            x.LastName = user.LastName;
            x.Admin = user.Admin;

            if (ModelState.IsValid)
            {
                var tmp = await _userManager.UpdateAsync(x);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost, ActionName("EditPwd")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPwd(string? id, WebApplication2User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            var x = await _userManager.FindByIdAsync(id);
            var pwd = await _userManager.GeneratePasswordResetTokenAsync(x);
            await _userManager.ResetPasswordAsync(x, pwd, user.PasswordHash);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(users);
            return RedirectToAction(nameof(Index));
        }
    }
}
