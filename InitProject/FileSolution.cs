﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitProject
{
    public class FileSolution
    {
        public void CreateDocumentationCOntroller()
        {
            string path = @"c:\usuarios\jamurillo\MyTest.txt";
            path = Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\File.txt");
            var current = Directory.GetCurrentDirectory();
            //"C:\\Clima_Laboral\\InitProject\\bin\\Debug"
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}