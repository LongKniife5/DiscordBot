using Discord;
using Discord.Commands;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;


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
			#endregion
			
			//i'm not sure, but it is probably important
			var commands = discord.GetService<CommandService>();
			
			#region ConversationCommands
			commands.CreateCommand("hello")
				.Do(async (e) => {
					await e.Channel.SendMessage("Leave me alone " + e.User.Name + " I know where you live!!");
			});

			commands.CreateCommand("How Ya Doin?")
				.Do(async (e) => {
					await e.Channel.SendMessage("Shut up and get back to work " + e.User.Name + "!");
				});

			commands.CreateCommand("Idea")
				.Do(async (e) => {
					await e.Channel.SendMessage(e.User.Name + " that is your job to find!");
				});
			#endregion

			#region listCommand
			commands.CreateCommand("list")
				.Do(async (e) => {
					PurgeChannel(e);
				});
			#endregion

			#region todoCommand
			commands.CreateCommand("todo").Parameter("listVar", ParameterType.Multiple)
				.Do(async (e) => {
					await AddToList(e);
					PurgeChannel(e);
				});
			#endregion

			#region testCommand
			commands.CreateCommand("test").Parameter("testSTring", ParameterType.Multiple)
				.Do(async (e) => {
					string message = "";
					for (int i = 0; i < e.Args.Length; i++)
					{
						message += e.Args[i].ToString() + " ";
					}
					await e.Channel.SendTTSMessage(message);
				});
			#endregion

			#region doneCommand
			commands.CreateCommand("done").Parameter("toRemove", ParameterType.Multiple)
				.Do(async (e) => {
					await takeFromList(e);
					PurgeChannel(e);
				});
			#endregion

			commands.CreateCommand("Save")
				.Do(async (e) => {
					await e.Channel.SendMessage("Saving...");
					Save(e);
				});

			commands.CreateCommand("Load")
				.Do(async (e) => {
					await e.Channel.SendMessage("Loading...");
					Load(e);
				});

						commands.CreateCommand("Delete")
				.Do(async (e) => {
					await e.Channel.SendMessage("Deleting...");
					Delete(e);
				});

			#region ThisLogsTheBotIn
			discord.ExecuteAndWait(async () =>
			{
				await discord.Connect("MzUzMTk2MzM1OTY2NDUzNzYw.DIsatw.eqGS8pnl-lEFQKPW7gxwB6ADmu8", TokenType.Bot);
			});
			#endregion
		}

		//Tells the console the state of the bot, not seen in discord
		private void Log(object sender, LogMessageEventArgs e)
		{
			Console.WriteLine(e.Message);
		}

		//When the todo command is used, this is called, and this calls the constructmessage function 
		//and adds what is returned into the list of thingsToDo
		private async Task AddToList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);
			toDoList.Add(message);
		}

		private async Task Save(CommandEventArgs e)
		{

			string thisFile = Directory.GetCurrentDirectory();
			string saveToPath = thisFile + "\\saveFile.txt";
			//await e.Channel.SendMessage(saveToPath);

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(saveToPath);

			UserData data = new UserData();
			int i = 0;
			foreach (string item in toDoList)
			{
				data.TODOLIST.Add(toDoList[i]);
				i++;
			}

			bf.Serialize(file, data);
			file.Close();
		}

		private async Task Load (CommandEventArgs e)
		{
			if(File.Exists(Directory.GetCurrentDirectory() + "\\saveFile.txt"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Directory.GetCurrentDirectory() + "\\saveFile.txt", FileMode.Open);

				UserData data = (UserData)bf.Deserialize(file);
				file.Close();

				int i = 0;
				foreach (string item in data.TODOLIST)
				{
					toDoList.Add(data.TODOLIST[i]);
					i++;
				}
			}
		}

		private async Task Delete(CommandEventArgs e)
		{
			if (File.Exists(Directory.GetCurrentDirectory() + "\\saveFile.txt"))
			{
				File.Delete(Directory.GetCurrentDirectory() + "\\saveFile.txt");
			}
		}

		//When the command is used, this is called and this calls the constructmessage function
		//and uses what is returned to remove that from the toDoList
		private async Task takeFromList(CommandEventArgs e)
		{
			var message = ConstructMessage(e);

			int n;
			bool isNumeric = int.TryParse(message, out n);
			if (isNumeric)
			{
				toDoList.Remove(toDoList[n-1]);
			}
			else
			{
				toDoList.Remove(message);
			}
			//await e.Channel.SendMessage(message + " is removed from the list");
		}

		//This turns what was said immediately after the 
		//command into a string and returns the value
		private string ConstructMessage(CommandEventArgs e)
		{
			string message = "";

			//if what was said after the commands had spaces (ex. .test hello world) 
			//this will make sure that all words are read, instead of just the first.
			for (int i = 0; i < e.Args.Length; i++)
			{
				message += e.Args[i].ToString() + " ";
			}
			return message;
		}

		//Despite the name, this does two things...
		//First it does clear a lot of what is in the channel, 100 messages to be precise as that is the limit
		//However after that it displays the ToDoList, so that we can always see what is on the list
		private async void PurgeChannel(CommandEventArgs e)
		{
			//clears the messages
			Message[] messagesToDelete;
			messagesToDelete = await e.Channel.DownloadMessages(100);

			await e.Channel.DeleteMessages(messagesToDelete);


			//displays the list
			string message = "```List: \n=========================== \n";
			int i = 0;
			foreach (string item in toDoList)
			{
                message += "- ";
				message += toDoList[i] + "\n";
				i++;
			}
			message += "```";
			await e.Channel.SendMessage(message);
		}

	}
}

[Serializable]
class UserData
{
	public List<string> TODOLIST = new List<string>();
}
