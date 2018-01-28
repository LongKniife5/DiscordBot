using Discord.Commands;
using System.Threading.Tasks;

namespace ToDoBot.Modules
{
    public class done : ModuleBase<SocketCommandContext>
    {
        [Command("done")]
        public async Task removeItem(string toRemove)
        {
            int n;
            bool isNumeric = int.TryParse(toRemove, out n);
            if(isNumeric)
            {
                Program.list.Remove(Program.list[n-1]);
            }
            else
            {
                Program.list.Remove(toRemove);
            }
            await ReplyAsync("removed item " + toRemove);
        }
    }
}
