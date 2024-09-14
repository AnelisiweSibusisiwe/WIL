using BumbleBeeFoundation.Models;
using Microsoft.EntityFrameworkCore;

namespace BumbleBeeFoundation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add the DbContext configuration to connect to your SQL Server database.
            builder.Services.AddDbContext<BumbleBeeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=BumbleBeeDB;Integrated Security=True;Trust Server Certificate=True;")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
