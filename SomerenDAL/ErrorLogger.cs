using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace SomerenDAL
{
    public static class ErrorLogger
    {
        //basic errorLogger
        public static void WriteLogToFile(string errorMessage)
        {
            //file path comes from the app.config file
            string filepath = ConfigurationManager.AppSettings["loggerFilepath"];

            StreamWriter errorWriter = new StreamWriter(filepath, true);
            //write the error with current datetime to the errorLog file
            errorWriter.WriteLine($"[{DateTime.Now}] : {errorMessage}");
            errorWriter.Close();
        }
    }
}
