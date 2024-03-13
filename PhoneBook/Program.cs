using Microsoft.EntityFrameworkCore;
using PhoneBook.DB;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.DB.Infrastructure;
using PhoneBook.DB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IPhoneBookData, PhoneBookData>();

            builder.Services.AddDefaultIdentity<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireUppercase = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}