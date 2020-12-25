using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
	public class CustomContextMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly CustomContext _customContext;

		public CustomContextMiddleware(RequestDelegate next, CustomContext customContext)
		{
			_next = next;
			_customContext = customContext;
		}

		public async Task Invoke(HttpContext context)
		{
			_customContext.Init();
			await _next.Invoke(context);
		}
	}

	public static class CustomContextMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomContext(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomContextMiddleware>();
		}
	}
}
