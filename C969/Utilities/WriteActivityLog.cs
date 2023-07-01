using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{
    public class WriteActivityLog
    {
        public static void Write(string logMessage)
        {
            // Get path to the user's documents folder
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Create a path to the log file
            string logFilePath = Path.Combine(documentsFolder, "ActivityLog.txt");

            // Write to the file (append if it already exists)
            using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
            {
                writer.WriteLine($"{DateTime.Now}: {logMessage}");
            }
        }
    }
}
