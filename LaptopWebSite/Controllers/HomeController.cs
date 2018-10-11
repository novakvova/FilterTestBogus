using LaptopWebSite.Models;
using LaptopWebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LaptopWebSite.Models.Entities;

namespace LaptopWebSite.Controllers
{
    public class HomeController : Controller
    {

        public readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Filters = GetListFilters();
            return View("List", model);
        }
        public ActionResult Filter(string[] fvalues)
        {
            HomeViewModel model = new HomeViewModel();
            model.Products = GetProductsByFilter(fvalues);
            model.Filters = GetListFilters();
            return View("List", model);
        }
        private List<FNameViewModel> GetListFilters()
        {
            var query = from f in _context.VFilterNameGroups.AsQueryable()
                        where f.FilterValueId != null
                        select new
                        {
                            FNameId = f.FilterNameId,
                            FName = f.FilterName,
                            FValueId = f.FilterValueId,
                            FValue = f.FilterValue
                        };
            var groupNames = from f in query
                             group f by new
                             {
                                 Id = f.FNameId,
                                 Name = f.FName
                             } into g
                             orderby g.Key.Name
                             select g;
            List<FNameViewModel> listGroupFilters = new List<FNameViewModel>();
            //listGroupFilters = 
            foreach (var filterName in groupNames)
            {
                FNameViewModel fName = new FNameViewModel
                {
                    Id = filterName.Key.Id,
                    Name = filterName.Key.Name
                };

                fName.Children = (from v in filterName
                                  group v by new FValueViewModel
                                  {
                                      Id = v.FValueId,
                                      Name = v.FValue
                                  } into g
                                  select g.Key).ToList();

                listGroupFilters.Add(fName);
            }
            return listGroupFilters;
        }

        private List<ProductItemViemModel> GetProductsByFilter(string[] values)
        {
            int[] filterValueSearchList= { }; 
            if(values!=null)
                filterValueSearchList= values.Select(v=>int.Parse(v)).ToArray();//{ 6, 7, 2 };
            var query = _context
                .Products
                .Include(f => f.Filtres)
                .AsQueryable();
            var filtersList = GetListFilters();
            foreach (var fName in filtersList)
            {
                int count = 0; //Кількість співпадінь у даній групі фільрів
                var predicate = PredicateBuilder.False<Product>();
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearchList.Length; i++)
                    {
                        var idV = fValue.Id;
                        if (filterValueSearchList[i] == idV)
                        {
                            predicate = predicate
                                .Or(p => p.Filtres
                                    .Any(f => f.FilterValueId == idV));
                            count++;
                        }
                    }
                }
                if (count != 0)
                    query = query.Where(predicate);
            }
            var listProductSearch = query.Select(p => new ProductItemViemModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Count = p.Count
            }).ToList();
            return listProductSearch;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}