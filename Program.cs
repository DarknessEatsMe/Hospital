using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Health;
using Health.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Health
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews(MvcOptions =>
			{
				MvcOptions.EnableEndpointRouting = false;
			});

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			 .AddCookie(
			 options =>
			 {
				options.LoginPath = "/Auth/Login";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.Cookie.MaxAge = options.ExpireTimeSpan;
				options.SlidingExpiration = true;
			 });
            builder.Services.AddAuthorization();
			builder.Services.AddSession();

			var app = builder.Build();

			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();

			app.UseMvcWithDefaultRoute();

            app.MapGet("/Auth/Exit", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Redirect("/");
            });

            app.Run();
		}
	}
}
