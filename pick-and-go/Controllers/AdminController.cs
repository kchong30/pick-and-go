using Microsoft.AspNetCore.Mvc;
using PickAndGo.Data;
using PickAndGo.Models;
using PickAndGo.ViewModels;
using System.Diagnostics;
using PickAndGo.Repositories;
using NuGet.Protocol;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PickAndGo.Controllers
{
    public class AdminController : Controller
    {
        private readonly PickAndGoContext _db;

        public AdminController(PickAndGoContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IngredientsIndex()
        {
            return View(_db.Ingredients.ToList());
        }

        public IActionResult IngredientsCreate()
        {
            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View();
        }

        [HttpPost]
        public IActionResult IngredientsCreate(IngredientVM ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Ingredients.Add(new Ingredient
                    {
                        Description = ingredient.Description,
                        Price = ingredient.Price,
                        CategoryId = ingredient.CategoryId,
                        InStock = ingredient.InStock
                    });
                    _db.SaveChanges();
                    return RedirectToAction("IngredientsIndex");
                }
            }
            catch
            {
                return View();
            }

            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View(ingredient);
        }

        public IActionResult IngredientsDetails(int id)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            var vm = iR.ReturnIngredientById(id);
            
            return View(vm);
        }

        public IActionResult IngredientsEdit(int? id)
        {
            Ingredient ingredient = _db.Ingredients.Find(id);
            ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");
            return View(ingredient);
        }

        [HttpPost]
        public IActionResult IngredientsEdit(IngredientVM ingredient)
        {
            IngredientsRepository iR = new IngredientsRepository(_db);
            try
            {
                if (ModelState.IsValid)
                {
                    iR.EditIngredient(new Ingredient
                    {
                        IngredientId = ingredient.IngredientId,
                        Description = ingredient.Description,
                        Price = ingredient.Price,
                        CategoryId = ingredient.CategoryId,
                        InStock = ingredient.InStock
                    });
                    HttpContext.Session.SetString("CategoryId", ingredient.CategoryId);
                }
                ViewData["categories"] = new SelectList(_db.Categories, "CategoryId", "CategoryId");

                return RedirectToAction("IngredientsDetails", "Ingredients", new { id = ingredient.IngredientId });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult CustomerList()
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnAllCustomers();

            return View(vm);
        }
        public IActionResult CustomerDetail(int customerId)
        {
            CustomerRepository cR = new CustomerRepository(_db);
            var vm = cR.ReturnCustomerById(customerId);

            return View(vm);
        }

        public IActionResult Overview()
        {
            OrderHeaderRepository ohRepo = new OrderHeaderRepository(_db);
            OrderHeaderVM ohVM = new OrderHeaderVM();

            ohVM.Pending = ohRepo.GetAll().Item1;
            ohVM.Completed = ohRepo.GetAll().Item2;

            return View(ohVM);
        }

    }
}