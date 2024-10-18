
using GameCenter.Project.Porject2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameCenter.Project.Porject2.Utils
{
    public static class DataHandler
    {
        static readonly string directory = Directory.GetParent(Environment.CurrentDirectory)!.ToString();
        static readonly string path = directory + @"../../../Project/Porject2/Data/users.json";
        static readonly string jsonString = File.ReadAllText(path);

        public static List<User> GetUsersList()
        {
            return JsonSerializer.Deserialize<List<User>>(jsonString)!;
        }

        public static List<User> UpdateList(List<User> usersList)
        {
            var serilized = JsonSerializer.Serialize(usersList);
            File.WriteAllText(path, serilized);
            return usersList;
        }
    }
}
///@"../../../Project/Project2/Data/users.json"