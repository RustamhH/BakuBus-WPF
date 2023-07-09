using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BakuBus.Models
{
    public static class JsonFileHandler
    {
        public static T Read<T>(string filePath) where T : new()
        {

            JsonSerializerOptions op = new JsonSerializerOptions();
            using FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            return JsonSerializer.Deserialize<T>(fs, op);


        }
        public static void Write<T>(string filePath, T values)
        {
            try
            {
                JsonSerializerOptions op = new JsonSerializerOptions();
                op.WriteIndented = true;
                File.WriteAllText(filePath, JsonSerializer.Serialize(values, op));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
