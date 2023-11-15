using machinetest.Data;
using machinetest.Models;
using machinetest.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace machinetest.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController (DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password, 10),
                RegistrationDateTime = DateTime.Now,
            };

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var usr = await _dataContext.Users
                         .FirstOrDefaultAsync(e => e.Username.ToLower() == model.Username.ToLower());

                if (usr == null || !BCrypt.Net.BCrypt.Verify(model.Password, usr.Password))
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(InputValue model)
        {
            if (ModelState.IsValid)
            {
                double fv = model.PV * Math.Pow((1 + (model.R / 100.0)), model.N);
                ViewBag.Result = $"FV = {fv}";
            }
            else
            {
                return View("Index", model);
            }

            return View("Index");
        }

    }
}
