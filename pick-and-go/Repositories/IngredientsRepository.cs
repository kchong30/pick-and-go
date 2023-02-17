﻿using Microsoft.EntityFrameworkCore;
using PickAndGo.ViewModels;
using PickAndGo.Models;
using System.Security.Principal;

namespace PickAndGo.Repositories
{
    public class IngredientsRepository
    {
        private readonly PickAndGoContext _db;

        public IngredientsRepository(PickAndGoContext context)
        {
            _db = context;
        }

        public IQueryable<IngredientListVM> BuildIngredientListVM()
        {
            var vmList = from c in _db.Categories
                         orderby c.CategoryId
                         select new IngredientListVM
                         {
                             CategoryId = c.CategoryId,
                             Ingredients = (List<IngredientVM>)(from i in _db.Ingredients
                                                                where c.CategoryId == i.CategoryId
                                                                orderby i.CategoryId
                                                                let oCount = (from l in _db.LineIngredients
                                                                                join ol in _db.OrderLines on l.LineId equals ol.LineId
                                                                                where l.IngredientId == i.IngredientId &&
                                                                                      ol.LineStatus == "O" select l).Count()
                                                                select new IngredientVM
                                                                {
                                                                    CategoryId = c.CategoryId,
                                                                    IngredientId = i.IngredientId,
                                                                    Description = i.Description,
                                                                    Price = i.Price,
                                                                    InStock = i.InStock,
                                                                    OutstandingOrders = (oCount > 0) ? true : false,
                                                                    InStockIcon = (i.InStock == "Y") ? "check.svg" : "x.svg",
                                                                }),
                         };

            return vmList;
        }

        public IEnumerable<IngredientVM> ReturnAllIngredients()
        {

            var vm = from i in _db.Ingredients
                     select new IngredientVM
                     {
                         IngredientId = i.IngredientId,
                         Description = i.Description,
                         Price = i.Price,
                         CategoryId = i.CategoryId,
                         InStock = i.InStock
                     };

            return vm;
        }

        public IngredientVM ReturnIngredientById(int ingredientId)
        {

            var vm = ReturnAllIngredients().Where(c => c.IngredientId == ingredientId).FirstOrDefault();
            vm.InStockIcon = (vm.InStock == "Y") ? "check.svg" : "x.svg";
            return vm;
        }

        public Ingredient GetIngredientRecord(int ingredientId)
        {
            var ingredient = _db.Ingredients.Where(i => i.IngredientId == ingredientId).FirstOrDefault();
            return ingredient;
        }

        public IngredientVM BuildIngredientVM(int ingredientId)
        {
            var ingredient = GetIngredientRecord(ingredientId);
            IngredientVM vm = new IngredientVM();
            vm.IngredientId = ingredient.IngredientId;
            vm.Description = ingredient.Description;
            vm.Price = ingredient.Price;
            vm.CategoryId = ingredient.CategoryId;
            vm.IngredientInStock = ingredient.InStock == "Y" ? true : false;
            return vm;
        }

        public string EditIngredient(IngredientVM ingredientVM)
        {
            string message = "";
            _db.Update(new Ingredient
            {
                IngredientId = ingredientVM.IngredientId,
                Description = ingredientVM.Description,
                Price = ingredientVM.Price,
                CategoryId = ingredientVM.CategoryId,
                InStock = ingredientVM.IngredientInStock == true ? "Y" : "N",
            }); ;
            _db.SaveChanges();
            return message;
        }

        public string DeleteIngredient(int ingredientId, string category)
        {
            string deleteMessage = "";
            Ingredient ingredient = GetIngredientRecord(ingredientId);

            try
            {
                _db.Ingredients.Remove(ingredient);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                deleteMessage = e.Message + " " + "The ingredient may not exist or "
                                                + "there could be a foreign key restriction.";
            }

            if (deleteMessage == "")
            {
                deleteMessage = $"** Ingredient {ingredient.Description} has been deleted " +
                                $"from category {category}.";
            }
            return deleteMessage;
        }

        public string CreateIngredient(IngredientVM ingredientVM)
        {
            string message = "";
            try
            {
                _db.Ingredients.Add(new Ingredient
                {
                    Description = ingredientVM.Description,
                    Price = ingredientVM.Price,
                    CategoryId = ingredientVM.CategoryId,
                    InStock = ingredientVM.InStock
                });
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            if (message == "")
            {
                message = $"** Ingredient {ingredientVM.Description} has been added " +
                               $"to category {ingredientVM.CategoryId}.";
            }
            return message;
        }
    }
}
