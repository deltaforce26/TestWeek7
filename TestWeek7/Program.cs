using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestWeek7.Data;
using TestWeek7.Services;
namespace TestWeek7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TestWeek7Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TestWeek7Context") ?? throw new InvalidOperationException("Connection string 'TestWeek7Context' not found.")));

            // Add services to the container.
            HttpClient client = new HttpClient();
            builder.Services.AddSingleton(new CrudService(client));
            builder.Services.AddControllersWithViews();
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
                pattern: "{controller=Todos}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
