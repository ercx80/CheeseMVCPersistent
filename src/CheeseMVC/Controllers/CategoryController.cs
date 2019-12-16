using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;//this object will be used to interact with objects in te database
        }
        public IActionResult Index()//this action method will render the index view in Category which will display all the categories
        {
            ViewBag.categories = context.Categories.ToList();

            return View();
            
        }
        public IActionResult Add()//this action method with no parameters will display the form to add a new category
        {

            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);

        }
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel) 
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory //property like format to create a name property
                {
                    Name = addCategoryViewModel.Name

                };
                context.Categories.Add(newCategory);//add new category to database
                context.SaveChanges();//save changes to database

                return Redirect("/");


            }
            return View(addCategoryViewModel);// if view model is not valid(validation failed), the program should return the add category viewmodel

        }
    }
}