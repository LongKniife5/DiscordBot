using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ToDoBot.Modules
{
    public class Save : ModuleBase<SocketCommandContext>
    {
        [Command("save")]
        public async Task _save()
        {
            // creates a binary formatter &  a file;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Directory.GetCurrentDirectory() + "/userInfo.dat");//Does not have to be .dat, but is what I went with

            // creates a object to save the data to
            UserData data = new UserData();

            foreach(string item in Program.list)
            {
                data.TODOLIST.Add(item);
            }
            foreach(string item in Program.list2)
            {
                data.List2.Add(item);
            }

            // writes the object to the file and closes it
            bf.Serialize(file, data);
            file.Close();
        }
    }
}
