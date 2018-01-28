using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace ToDoBot.Modules
{
    public class list : ModuleBase<SocketCommandContext>
    {
        [Command("list")]
        public async Task sayList()
        {
            string message = "";
            EmbedBuilder builder = new EmbedBuilder();
            for(int i = 0; i < Program.list.Count; i++)
            {
                message += "-";
                message += Program.list[i];
                message += "\n";
            }
            builder.WithTitle("List: ")
                .WithDescription(message)
                .WithColor(Color.Blue);

            await ReplyAsync("",false, builder.Build());
        }
    }
}
