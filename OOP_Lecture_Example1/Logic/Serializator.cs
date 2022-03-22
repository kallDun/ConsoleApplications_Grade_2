using Newtonsoft.Json;
using System.IO;

namespace OOP_Lecture_Example1.Logic
{
    class Serializator<T>
    {
        private readonly string path;
        public Serializator(string path)
        {
            this.path = path;
        }

        public void Serialize(T model)
        {
            var json_text = JsonConvert.SerializeObject(model);
            File.WriteAllText(path, json_text);
        }
        public T Deserialize()
        {
            var json_text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json_text);
        }
    }
}