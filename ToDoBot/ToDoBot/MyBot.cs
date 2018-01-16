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
		private IServiceProvider services;

		public static readonly string CommandPrefix = "."; 

		List<string> toDoList = new List<string>();

		public async Task Start()
		{
			client = new DiscordClient();
			commands = new CommandService();

			services = new ServiceCollection()
				.AddSingleton(commands)
				.BuildServiceProvider();

			await InstallCommands();
			
			await client.LoginAsync(TokenType.Bot, put token here);
			await client.SetGameAsync("hi");//i think it will say this
			await client.StartAsync();

			Console.WriteLine("ready");

			await Task.Delay(-1);
		}

		private async Task InstallCommands()
		{
			client.MessageReceived += HandleCommand;
			await commands.AddModulesAsync(Assembly.GetEntryAssembly());
		}

		private async Task HandleCommand(SocketMessage msg)
		{
			SocketUserMessage message = msg as SocketUserMessage;
			if(message == null)
				return;
			int argPos = 0;

			 if ((message.HasStringPrefix (CommandPrefix, ref argPos) || message.HasMentionPrefix (client.CurrentUser, ref argPos)) == false) return;

            CommandContext context = new CommandContext (client, message);
            CommandInfo executedCommand = commands.Search (context, argPos).Commands [0].Command;

			IResult result = await commands.ExecuteAsync (context, argPos, services);
            if (result.IsSuccess == false)
            {
                if (result.Error == CommandError.UnknownCommand)
					return;
                if (result.Error == CommandError.BadArgCount)
                {
                    EmbedBuilder eb = new EmbedBuilder ();
                    eb.WithTitle ("ERROR: Bad argument count");
                    eb.WithColor (Color.Red);
                    await context.Channel.SendMessageAsync (string.Empty, false, eb);
                    await commands.ExecuteAsync (context, $"command {executedCommand.Name}");
                }
                else
                    await context.Channel.SendMessageAsync (result.ErrorReason);
            }
		}
	}
}