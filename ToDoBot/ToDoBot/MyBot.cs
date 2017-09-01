using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ToDoBot
{
	class MyBot
	{
		DiscordClient discord;

		List<string> toDoList = new List<string>();


		public MyBot()
		{
			discord = new DiscordClient(x =>
			{
				x.LogLevel = LogSeverity.Info;
				x.LogHandler = Log;
			});


			discord.UsingCommands(x =>
			{
				x.PrefixChar = '.';
				x.AllowMentionPrefix = true;
			});


			var commands = discord.GetService<CommandService>();


			commands.CreateCommand("hello")
				.Do(async (e) => {
					await e.Channel.SendMessage("Leave me alone " + e.User.Name + " I know where you live!!");
			});


			commands.CreateCommand("How Ya Doin?")
				.Do(async (e) => {
					await e.Channel.SendMessage("Shut up and get back to work " + e.User.Name + "!");
				});


			commands.CreateCommand("list")
				.Do(async (e) => {
					PurgeChannel(e);
				});


			commands.CreateCommand("todo").Parameter("listVar", ParameterType.Required)
				.Do(async (e) => {
					await AddToList(e);
					PurgeChannel(e);
				});


			commands.CreateCommand("done").Parameter("toRemove", ParameterType.Required)
				.Do(async (e) => {
					await takeFromList(e);
					PurgeChannel(e);
				});


			discord.ExecuteAndWait(async () =>
			{
				await discord.Connect("MzUzMTk2MzM1OTY2NDUzNzYw.DIsatw.eqGS8pnl-lEFQKPW7gxwB6ADmu8", TokenType.Bot);
			});
		}


		private void Log(object sender, LogMessageEventArgs e)
		{
			Console.WriteLine(e.Message);
		}



		private async Task AddToList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);
			toDoList.Add(message);
		}


		private async Task takeFromList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);
			//await e.Channel.SendMessage(message + " is removed from the list");
			toDoList.Remove(message);
		}


		private string ConstructMessage(CommandEventArgs e)
		{
			string message = "";

			for (int i = 0; i < e.Args.Length; i++)
			{
				message += e.Args[i].ToString();
			}
			return message;
		}


		private async void PurgeChannel(CommandEventArgs e)
		{
			Message[] messagesToDelete;
			messagesToDelete = await e.Channel.DownloadMessages(100);

			await e.Channel.DeleteMessages(messagesToDelete);

			await e.Channel.SendMessage("```Test:```");
			await e.Channel.SendMessage("```==========================```");
		
			foreach (string item in toDoList)
			{
				await e.Channel.SendMessage("```" + "- " + item + "```");
			}
		}
	}
}
