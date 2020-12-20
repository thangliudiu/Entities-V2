using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    public class FileHandle
    {
        public static void WriteFile(string pathfile,string text)
        {
            StreamWriter writer = new StreamWriter(pathfile);
            writer.Write(text);
            writer.Close();
        }
        public static string GetTextFile(string pathfile)
        {
            string result = "";
            if (File.Exists(pathfile) == false) return result;
            StreamReader reader = new StreamReader(pathfile);
            result =  reader.ReadToEnd();
            reader.Close();

            return result;
        }
    }
}
