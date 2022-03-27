using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace ErrorHandlers
{
    public static class ErrorLogger
    {
        public static void WriteLogToFile(string errorMessage)
        {
            string filepath = ConfigurationManager.AppSettings["loggerFilepath"];
            StreamWriter errorWriter = new StreamWriter(filepath, true);
            errorWriter.WriteLine($"[{DateTime.Now}] : {errorMessage}");
            errorWriter.Close();
        }
    }
}
