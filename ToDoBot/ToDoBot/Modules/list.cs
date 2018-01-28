using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                message += Program.list[i];
                message += "\n";
            }
            builder.WithTitle("Ping!")
                .WithDescription(message)
                .WithColor(Color.Blue);

            await ReplyAsync("",false, builder.Build());
        }
    }
}
