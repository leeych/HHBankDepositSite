using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public class LogHelper
    {
        private static readonly string LogPath = ".\\Log\\";
        private static readonly string WriteLogStatus = "0";

        public static void WriteToFile(string logContent, bool success)
        {
            if (WriteLogStatus == "1" || (WriteLogStatus == "2" && !success))
            {
                StreamWriter writer = null;
                try
                {
                    string directory = LogPath;
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    string filePath = string.Format("{0}\\{1}.log", LogPath, DateTime.Now.ToString("yyyyMMddHH"));
                    writer = new StreamWriter(filePath, true, Encoding.UTF8);
                    writer.WriteLine(logContent);
                }
                catch (Exception)
                {
                    // return;
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }
    }
}
