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
    public class StoreController : Controller
    {
        private IBL _bl;
        public StoreController(IBL bl)
        {
            _bl = bl;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            List<StoreFrontVM> allCusto = _bl.GetAllStoreFronts()
                                            .Select(s => new StoreFrontVM(s)).ToList();
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
        public ActionResult Create(StoreFrontVM store)
        {
            try
            {
                // if data is valid
                if (ModelState.IsValid)
                {
                    _bl.AddStoreFront(store.ToModel());
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
            return View(new StoreFrontVM(_bl.GetStoreFront(id)));
        }

        // POST: PopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreFrontVM store)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _bl.UpdateStore(store.ToModel());
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
            return View(new StoreFrontVM(_bl.GetStoreFront(id))); 
        }

        // POST: PopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.RemoveStore(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
