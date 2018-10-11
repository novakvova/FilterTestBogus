using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaptopWebSite.ViewModels
{
    public class HomeViewModel
    {
        public List<FNameViewModel> Filters { get; set; }
        public List<ProductItemViemModel> Products { get; set; }
    }
}