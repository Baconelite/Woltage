using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woltage.Services
{
    public  class IOService
    {
        public void WriteToFile(string text, string fileName)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/{fileName}";
            // Check if the file already exists
            if (File.Exists(filePath))
            {
                // If the file exists, delete it
                File.Delete(filePath);
            }

            // Create a new file and write the text to it
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write(text);
            }
        }

        public T ReadFromFile<T>(string fileName)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/{fileName}";

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            // Read the text from the file and deserialize it
            string json = File.ReadAllText(filePath);
            T obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public List<string> ReadLinesFromFile(string fileName)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/{fileName}";
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return lines;
        }
    }
}
