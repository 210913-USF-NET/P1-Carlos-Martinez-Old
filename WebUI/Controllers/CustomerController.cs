using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private IBL _bl;
        public CustomerController(IBL bl)
        {
            _bl = bl;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            List<CustomerVM> allCusto = _bl.GetAllCustomers()
                                            .Select(c => new CustomerVM(c)).ToList();
            return View(allCusto);
            //List<Customer> allCustos = _bl.GetAllCustomers();
            //return View(allCustos);
        }

        // GET: PopController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customer)
        {
            try
            {
                // if data is valid
                if (ModelState.IsValid)
                {
                    _bl.AddCustomer(customer.ToModel());
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: PopController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new CustomerVM(_bl.GetCustomer(id)));
        }

        // POST: PopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerVM customer)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _bl.UpdateCustomer(customer.ToModel());
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: PopController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new CustomerVM(_bl.GetCustomer(id)));
        }

        // POST: PopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.RemoveCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
