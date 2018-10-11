namespace LaptopWebSite.Migrations
{
    using Bogus;
    using LaptopWebSite.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<LaptopWebSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LaptopWebSite.Models.ApplicationDbContext context)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            string baseDir = Path.GetDirectoryName(path) + "\\Models\\Entities\\SqlView\\vFilterNameGroups.sql";
            context.Database.ExecuteSqlCommand(File.ReadAllText(baseDir));

            #region InitFilterName
            context.FilterNames.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new FilterName
                {
                    Id = 1,
                    Name = " ÓÎ≥"
                });
            context.FilterNames.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new FilterName
                {
                    Id = 2,
                    Name = "–ÓÁÏ≥"
                });


            //context.SaveChanges();
            #endregion

            #region InitFilterValue
            context.FilterValues.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new FilterValue
                {
                    Id = 1,
                    Name = "L"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 2,
                    Name = "M"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 3,
                    Name = "XL"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 4,
                    Name = "XX"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 5,
                    Name = "◊ÓÌËÈ"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 6,
                    Name = "¡≥ÎËÈ"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 7,
                    Name = "«ÂÎÂÌËÈ"
                });
            context.FilterValues.AddOrUpdate(
                h => h.Id,
                new FilterValue
                {
                    Id = 8,
                    Name = "∆Ó‚ÚËÈ"
                });

            //context.SaveChanges();
            #endregion

            #region InitFilterNameGroups
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 1,
                    FilterValueId = 5
                });
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 1,
                    FilterValueId = 6
                });

            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 1,
                    FilterValueId = 7
                });
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 1,
                    FilterValueId = 8
                });

            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 2,
                    FilterValueId = 1
                });
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 2,
                    FilterValueId = 2
                });
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 2,
                    FilterValueId = 3
                });
            context.FilterNameGroups.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId },
                new FilterNameGroup
                {
                    FilterNameId = 2,
                    FilterValueId = 4
                });
            context.SaveChanges();
            #endregion

            #region InitProduct
            context.Products.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new Product
                {
                    Id = 1,
                    Name = "ƒÊËÌÒË",
                    Price = 240,
                    Count = 7,
                    Description=" ËÚ‡ÈÒ¸Í≥ ————",
                    IsAvailable=true
                });
            context.Products.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new Product
                {
                    Id = 2,
                    Name = "¡˛Í≥",
                    Price = 140,
                    Description = " ËÚ‡ÈÒ¸Í≥ ————",
                    IsAvailable = true
                });

            context.Products.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new Product
                {
                    Id = 3,
                    Name = "“ÛÒË",
                    Price = 1040,
                    Description = " ËÚ‡ÈÒ¸Í≥ ————",
                    IsAvailable = true
                });
            context.Products.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
                new Product
                {
                    Id = 4,
                    Name = "Ã‡ÈÍ≥",
                    Price = 40,
                    Description = " ËÚ‡ÈÒ¸Í≥ ————",
                    IsAvailable = true
                });
            context.SaveChanges();

            //List<Product> produtctsBogus = new List<Product>();
            var productIds = 5;
            var testOrders = new Faker<Product>("ru")
                .RuleFor(p => p.Id, f => productIds++)
                .RuleFor(p => p.IsAvailable, f => true)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Lorem.Text())
                .RuleFor(p=>p.Price, f=> Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p => p.Count, f => f.Random.Number(1, 10));
            for (int j = 0; j < 1000; j++)
            {
                var product = testOrders.Generate();
                context.Products.AddOrUpdate(
                h => h.Id,   // Use Name (or some other unique field) instead of Id
               product);
            }
            context.SaveChanges();
            #endregion

            #region InitFilter

            context.Filters.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                new Filter
                {
                    FilterNameId = 1,
                    FilterValueId = 6,
                    ProductId = 4
                });

            context.Filters.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                new Filter
                {
                    FilterNameId = 1,
                    FilterValueId = 7,
                    ProductId = 4
                });
            context.Filters.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                new Filter
                {
                    FilterNameId = 2,
                    FilterValueId = 2,
                    ProductId = 4
                });
            context.Filters.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                new Filter
                {
                    FilterNameId = 1,
                    FilterValueId = 6,
                    ProductId = 2
                });
            context.Filters.AddOrUpdate(
                h => new { h.FilterNameId, h.FilterValueId, h.ProductId },
                new Filter
                {
                    FilterNameId = 1,
                    FilterValueId = 7,
                    ProductId = 1
                });
            //var testFilters = new Faker<Filter>()
            //    .RuleFor(p => p.ProductId, f => f.Random.Number(5, 1000))
            //    .RuleFor(p => p.FilterNameId, f => f.Random.Number(1, 2))
            //    .RuleFor(p => p.FilterValueId, (f, p) =>
            //    {
            //        if(p.FilterNameId==1)
            //         return f.Random.Number(5, 8);
            //        else
            //            return f.Random.Number(1, 4);
            //    });
            //for (int i = 0; i < 2000; i++)
            //{
            //    var f = testFilters.Generate();
            //    var filterDb = context.Filters
            //        .SingleOrDefault(g => g.FilterValueId == f.FilterValueId &&
            //        g.FilterNameId == f.FilterNameId && g.ProductId == f.ProductId);
            //    if(filterDb==null)
            //    {
            //        context.Filters.Add(f);
            //        context.SaveChanges();
            //    }
            //}
            //context.SaveChanges();
            #endregion
        }
    }
}
