using Discord.Commands;
using System.Threading.Tasks;

namespace ToDoBot
{
    public class Todo : ModuleBase<SocketCommandContext>
    {
        [Command("Todo")]
        public async Task addToList([Remainder]string toAdd)
        {
            Program.list.Add(toAdd);
            await ReplyAsync("Adding " + toAdd + " to the list.");
        }
    }
}
