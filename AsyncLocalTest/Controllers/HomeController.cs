using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test1.Models;

namespace Test1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CustomContext _customContext;

		public HomeController(ILogger<HomeController> logger, CustomContext customContext)
		{
			_logger = logger;
			_customContext = customContext;
		}

		public async Task<IActionResult> Index()
		{
			_customContext.Items["test"] = Guid.NewGuid().ToString();

			await Task.Delay(5000);

			return Redirect("/");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
