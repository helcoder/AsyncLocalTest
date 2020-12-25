using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Test1.Models
{
	public class CustomContext : IDisposable
	{
		private static AsyncLocal<ConcurrentDictionary<string, object>> _items = new AsyncLocal<ConcurrentDictionary<string, object>>();

		public ConcurrentDictionary<string, object> Items
		{
			get
			{
				if (_items.Value == null)
				{
					_items.Value = new ConcurrentDictionary<string, object>();
				}

				return _items.Value;
			}
			set
			{
				_items.Value = value;
			}
		}

		public void Dispose()
		{
			Items = null;
		}

		public void Init()
		{
			Items = new ConcurrentDictionary<string, object>();
		}
	}
}
