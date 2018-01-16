using Discord;
using Discord.Commands;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoBot
{
	public class MyBot
	{
		private DiscordClient client;
		private CommandService commands;

		public static readonly string CommandPrefix = "."; 

		List<string> toDoList = new List<string>();

		public async Task Start()
		{
			client = new DiscordClient();
			commands = new CommandService();

			await InstallCommands();
			
			await client.LoginAsync(TokenType.Bot, put token here);
			await client.SetGameAsync("hi");//i think it will say this
			await client.StartAsync();

			Console.WriteLine("ready");

			await Task.Delay(-1);
		}
	}
}