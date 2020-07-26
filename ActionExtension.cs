using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
	public static class ActionExtension
	{

		public static T Then<T>(this Action instance) where T : class, new()
		{
			T x = new T();
			return x;
		}
		public static T Catch<T>(this Action instance) where T : class, new()
		{
			T x = new T();
			return x;
		}
	}
}
