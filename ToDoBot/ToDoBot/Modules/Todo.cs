using Discord.Commands;
using System.Threading.Tasks;

namespace ToDoBot
{
    public class Todo : ModuleBase<SocketCommandContext>
    {
        [Command("Todo")]
        public async Task PingAsync()
        {
            await ReplyAsync(Context.Message.Content);
        }
    }
}
