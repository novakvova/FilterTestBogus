using Hangfire;
using LaptopWebSite.Controllers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaptopWebSite.Startup))]
namespace LaptopWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //GlobalConfiguration.Configuration
            //    .UseSqlServerStorage("DefaultConnection");
            //ProductController obj = new ProductController();
            //app.UseHangfireDashboard("/myJobDashbord", new DashboardOptions()
            //{
            //    Authorization = new [] {new HangfireAuthorizationFilter()}
            //});
            //RecurringJob.AddOrUpdate(
            //        () => obj.ClearImage(),Cron.Minutely() // Cron.Daily(3,0)
            //    );
            //app.UseHangfireServer();
        }
    }
}
