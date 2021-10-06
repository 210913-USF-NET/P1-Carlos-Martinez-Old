using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using Serilog;
using BL;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBL _bl;
        public HomeController(ILogger<HomeController> logger, IBL bl)
        {
            _logger = logger;
            _bl = bl;
        }

        public ActionResult SignIn(string username, string password)
        {
            if (username is null || password is null)
            {
                Log.Information($"Username or Password were null");
                return View("Index");
            }

            if (username.Length == 1)
            {
                username = username.ToUpper();
            }
            else
            {
                username = username[0].ToString().ToUpper() + username.Substring(1).ToLower();
            }

            // get all users
            List<CustomerVM> allCustos = _bl.GetAllCustomers()
                                            .Select(c => new CustomerVM(c)).ToList();

            // get the password for the respective user
            string hash = _bl.GetAllCustomers()
                                      .Where(c => c.Name == username)
                                      .Select(c => new CustomerVM(c).Password).FirstOrDefault();

            bool verify = false;
            if (hash is not null)
            { 
                verify = _bl.Verify(password, hash);
            }
            else
            {
                Log.Information($"HomeController/SignIn, string hash was null.");
                return View("Index");
            }

            // if login succeeds
            if (verify)
            {
                Log.Information($"{username} signed in!");
                return View("Privacy");
            }
            else
            {
                Log.Information("HomeController/SignIn, verify failed");
                return View("Index");
            }
        }

        public ActionResult NewCustomer()
        {
            return View("NewCustomer");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCustomer(CustomerVM customer)
        {
            try
            {
                // if data is valid
                if (ModelState.IsValid)
                {
                    if (customer.Name.Length == 1)
                    {
                        customer.Name = customer.Name.ToUpper();
                    }
                    else
                    {
                        customer.Name = customer.Name[0].ToString().ToUpper() + customer.Name.Substring(1).ToLower();
                    }

                    customer.Password = _bl.Hash(customer.Password);
                    _bl.AddCustomer(customer.ToModel());
                    Log.Information("Created a new customer!.");
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                Log.Warning("Creating customer failed.");
                return View("NewCustomer");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
