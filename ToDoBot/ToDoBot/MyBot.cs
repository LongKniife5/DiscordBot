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
			#region IHonestlyCouldn'tTellYouWhatThisDoes
			discord = new DiscordClient(x =>
			{
				x.LogLevel = LogSeverity.Info;
				x.LogHandler = Log;
			});
			#endregion

			#region ThisSetsUpTheCommandPrefix
			discord.UsingCommands(x =>
			{
				x.PrefixChar = '.';
				x.AllowMentionPrefix = true;
			});
<<<<<<< HEAD
			#endregion
			
			//i'm not sure, but it is probably important
			var commands = discord.GetService<CommandService>();

			#region ConversationCommands
=======



			var commands = discord.GetService<CommandService>();
			


>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			commands.CreateCommand("hello")
				.Do(async (e) => {
					await e.Channel.SendMessage("Leave me alone " + e.User.Name + " I know where you live!!");
			});


			commands.CreateCommand("How Ya Doin?")
				.Do(async (e) => {
					await e.Channel.SendMessage("Shut up and get back to work " + e.User.Name + "!");
				});
			#endregion

<<<<<<< HEAD
			#region listCommand
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			commands.CreateCommand("list")
				.Do(async (e) => {
					PurgeChannel(e);
				});
			#endregion

<<<<<<< HEAD
			#region todoCommand
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			commands.CreateCommand("todo").Parameter("listVar", ParameterType.Required)
				.Do(async (e) => {
					await AddToList(e);
					PurgeChannel(e);
				});
			#endregion

<<<<<<< HEAD
			#region testCommand
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			commands.CreateCommand("test").Parameter("testSTring", ParameterType.Multiple)
				.Do(async (e) => {
					for (int i = 0; i < e.Args.Length; i++)
					{
						await e.Channel.SendMessage(e.Args[i].ToString());
					}
				});
			#endregion

			#region doneCommand
			commands.CreateCommand("done").Parameter("toRemove", ParameterType.Required)
				.Do(async (e) => {
					await takeFromList(e);
					PurgeChannel(e);
				});
			#endregion

<<<<<<< HEAD
			#region IBeleiveThisLogsTheBotIn
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			discord.ExecuteAndWait(async () =>
			{
				await discord.Connect("MzUzMTk2MzM1OTY2NDUzNzYw.DIsatw.eqGS8pnl-lEFQKPW7gxwB6ADmu8", TokenType.Bot);
			});
			#endregion
		}

<<<<<<< HEAD
		//Tells the console the state of the bot, not seen in discord
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
		private void Log(object sender, LogMessageEventArgs e)
		{
			Console.WriteLine(e.Message);
		}

<<<<<<< HEAD
		//When the todo command is used, this is called, and this calls the constructmessage function 
		//and adds what is returned into the list of thingsToDo
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
		private async Task AddToList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);
			toDoList.Add(message);
		}

<<<<<<< HEAD
		//When the command is used, this is called and this calls the constructmessage function
		//and uses what is returned to remove that from the toDoList
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
		private async Task takeFromList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);
			//await e.Channel.SendMessage(message + " is removed from the list");
			toDoList.Remove(message);
		}

<<<<<<< HEAD
		//This turns what was said immediately after the 
		//command into a string and returns the value
=======

>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
		private string ConstructMessage(CommandEventArgs e)
		{
			string message = "";

			//if what was said after the commands had spaces (ex. .test hello world) 
			//this will make sure that all words are read, instead of just the first.
			for (int i = 0; i < e.Args.Length; i++)
			{
				message += e.Args[i].ToString();
			}
			return message;
		}

<<<<<<< HEAD
		//Despite the name, this does two things...
		//First it does clear a lot of what is in the channel, 100 messages to be precise as that is the limit
		//However after that it displays the ToDoList, so that we can always see what is on the list
=======


>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
		private async void PurgeChannel(CommandEventArgs e)
		{
			//clears the messages
			Message[] messagesToDelete;
			messagesToDelete = await e.Channel.DownloadMessages(100);

			await e.Channel.DeleteMessages(messagesToDelete);

<<<<<<< HEAD

			//displays the list
			await e.Channel.SendMessage("```Test:```");
=======
			await e.Channel.SendMessage("```List:```");
>>>>>>> 345458d488051dcb6d8afa4c4be86ea1522fcc74
			await e.Channel.SendMessage("```==========================```");
		
			foreach (string item in toDoList)
			{
				await e.Channel.SendMessage("```" + "- " + item + "```");
			}
		}
	}
}
