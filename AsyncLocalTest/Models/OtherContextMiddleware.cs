using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.Models
{
	public class OtherContextMiddleware
	{
		private readonly RequestDelegate _next;

		public OtherContextMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next.Invoke(context);
		}
	}

	public static class OtherContextMiddlewareExtensions
	{
		public static IApplicationBuilder UseOtherContext(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<OtherContextMiddleware>();
		}
	}
}
