using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoBot.Modules
{
    public class Purge : ModuleBase<SocketCommandContext>
    {
        [Command("Purge")]
        public async Task clearMessages()
        {
            foreach (var Item in await Context.Channel.GetMessagesAsync(10).Flatten())
            {
                await Item.DeleteAsync();
            }
        }
    }
}
