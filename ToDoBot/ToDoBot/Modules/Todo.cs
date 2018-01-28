using Discord.Commands;
using System.Threading.Tasks;

namespace ToDoBot
{
    public class Todo : ModuleBase<SocketCommandContext>
    {
        [Command("Todo")]
        public async Task PingAsync(string toAdd)
        {
            Program.list.Add(toAdd);
            await ReplyAsync(Program.list[0]);
        }
    }
}
