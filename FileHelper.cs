using System;
using System.IO;
using System.Collections.Generic;

namespace fb_hacker_cup_2021.helpers
{
    internal class FileHelper
    {
        public List<string> ReadFile(string inputFilePath)
        {
            var returnList = new List<string>();
            try
            {
                string line = "";
                using (var sr = new StreamReader(inputFilePath))
                {
                    line = sr.ReadLine();
                    returnList.Add(line);

                    while (line != null)
                    {
                        line = sr.ReadLine();
                        returnList.Add(line);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }

            return returnList;
        }

        public List<string> WriteToFile(List<string> outputData, string outputFilePath)
        {
            var returnList = new List<string>();
            try
            {
                using (var sw = new StreamWriter(outputFilePath))
                {
                    outputData.ForEach(data => sw.WriteLine(data));
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }

            return returnList;
        }
    }
}