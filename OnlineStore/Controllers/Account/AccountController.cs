using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers.Account
{
    [Route("/[controller]/[action]")]
    public class AccountController : Controller
    {
        public AccountController(UsersDbContext context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private readonly UsersDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel um = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    ReserveEmail = model.ReserveEmail,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Year = model.Year,
                    Month = model.Month,
                    Day = model.Day,
                    PhoneNumber = model.Phone,
                    Roles = model.Roles
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(um, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(um, false);
                    return Ok("create");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return ValidationProblem();
        }
        
        public IActionResult Edit()
        {
            return NotFound("edit");
        }
        
        public IActionResult Remove()
        {
            return NotFound("remove");
        }

        public IActionResult Login()
        {
            var l = _context.UsersModel.ToList();
            return NotFound("login");
        }

        public IActionResult Logout()
        {
            return NotFound("logout");
        }
    }
}
