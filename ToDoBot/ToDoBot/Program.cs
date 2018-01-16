using System.Threading.Tasks;

namespace ToDoBot
{
	class Program
	{
		static void Main(string[] args)
		{
			MyBot bot = new MyBot();
			await bot.Start();

			await Task.Delay(-1);
		}
	}
}
