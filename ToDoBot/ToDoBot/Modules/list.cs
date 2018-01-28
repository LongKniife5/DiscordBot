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
            for(int i = 0; i < Program.list.Count; i++)
            {
                message += Program.list[i];
            }
            await ReplyAsync(message);
        }
    }
}
