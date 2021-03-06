using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test1.Models;

namespace Test1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddSingleton<CustomContext>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomContext customContext)
		{
			app.Use((context, next) =>
			{
				customContext.Init();
				context.Response.RegisterForDispose(customContext);
				return next();
			});

			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					await Task.Delay(3000);
					//context.Response.Redirect("/");
				});
			});

			app.UseStaticFiles();

			app.UseRouting();

			//app.UseCustomContext();

			app.UseOtherContext();

			app.Use((context, next) =>
			{
				context.Response.OnStarting(state => {
					var test = customContext.Items["test"];

					return Task.CompletedTask;
				}, context);

				return next();
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
