using LaptopWebSite.Models;
using LaptopWebSite.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopWebSite.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ContentResult GetParentCategories()
        {
            var categories =
                _context.Categories
                .Where(c => c.ParentId == null)
                .Select(c => new
                {
                    id = c.Id,
                    parent = "#",
                    text = c.Name,
                    children = c.Children.Count() != 0
                }).ToList();
            string json = JsonConvert.SerializeObject(categories);

            return Content(json, "application/json");
        }
        [HttpGet]
        public ContentResult GetChildrenCategories(int id)
        {
            var categories =
                _context.Categories
                .Where(c => c.ParentId == id)
                .Select(c => new
                {
                    id = c.Id,
                    parent = id,
                    text = c.Name,
                    children = c.Children.Count() != 0
                }).ToList();
            string json = JsonConvert.SerializeObject(categories);

            return Content(json, "application/json");
        }
        [HttpGet]
        public ContentResult CreateCategory(string name, int? parentId)
        {
            var category = new Category
            {
                ParentId=parentId,
                Name = name
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
                
            string json = JsonConvert.SerializeObject(category.Id);

            return Content(json, "application/json");
        }
    }
}