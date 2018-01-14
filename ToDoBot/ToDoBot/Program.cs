using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoBot
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			MyBot bot = new MyBot();
			await bot.Start();

			await Task.Delay(-1);
		}
	}
}
