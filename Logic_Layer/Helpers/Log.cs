using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic_Layer.Helpers
{
    public class Log
    {
        private static Log instance = null;
        public static Log Instance
        {
            get
            {
                if (instance == null)
                    instance = new Log(@"C:\Log_PrimeStone\");
                return instance;
            }
        }
        private string Path = "";
        public Log(string Path)
        {
            this.Path = Path;
        }
        public void Add(string sLog)
        {
            CreateDirector();
            string name = GetnameFile();
            string cadena = "";
            cadena += DateTime.Now + "-" + sLog + Environment.NewLine;
            StreamWriter sw = new StreamWriter($"{Path}/{name}", true);
            sw.Write(cadena);
            sw.Close();
        }
        private string GetnameFile()
        {
            string name = "";
            name = $"log{DateTime.Now.Year}.txt";
            return name;
        }
        public void CreateDirector()
        {
            try
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                string errro = ex.Message + "---" + ex.StackTrace;
                throw new Exception(ex.Message);
            }
        }
    }
}
