using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Piksel.Testing
{
    public static class Utils
    {
		/// <summary>
		/// Repeat <paramref name="action"/> <paramref name="count"/> times
		/// </summary>
		/// <param name="count">How many times the action will be repeated</param>
		/// <param name="action">The action to perform every cycle, given the 0-based index as it's first parameter</param>
		public static void Repeat(int count, Action<int> action)
		{
			foreach (var index in Enumerable.Range(0, count))
			{
				action(index);
			}
		}
	}
}
