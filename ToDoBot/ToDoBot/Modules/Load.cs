using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace ToDoBot.Modules
{
    public class Load : ModuleBase<SocketCommandContext>
    {
        [Command("Load")]
        public async Task load()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/userInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Directory.GetCurrentDirectory() + "/userInfo.dat", FileMode.Open);

                UserData data = (UserData)bf.Deserialize(file);
                file.Close();

                foreach(string item in data.TODOLIST)
                {
                    Program.list.Add(item);
                }
            }
        }
    }
}
