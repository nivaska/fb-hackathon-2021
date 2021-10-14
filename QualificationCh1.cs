using System;
using System.Collections.Generic;
using System.Linq;
using fb_hacker_cup_2021.helpers;

namespace fb_hacker_cup_2021
{
    internal static class Qualification_ch1
    {
        // Qualification - Task 1 - String Consistency
        public static void Run(string inputFilePath)
        {
            var fileHelper = new FileHelper();
            var inputData = fileHelper.ReadFile(inputFilePath);
            var inputCount = int.Parse(inputData[0]);

            var outputData = new List<string>();
            for (int i = 1; i <= inputCount; i++)
            {
                var str = inputData[i];
                var group = str.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count(), IsVowel = c.Key.IsCharVowel() });
                var totalCount = str.Length;
                var vowelCount = group.Where(x => x.IsVowel).Sum(x => x.Count);
                var consonantCount = totalCount - vowelCount;
                var consistencyOut = new List<int>();
                foreach (var ch in group)
                {
                    consistencyOut.Add(getConisitency(ch.IsVowel ? vowelCount : consonantCount,
                    ch.Count, totalCount));
                }

                outputData.Add($"Case #{i}: {consistencyOut.Min()}");
            }

            fileHelper.WriteToFile(outputData, "out_qual_a1.txt");

            int getConisitency(int groupCount, int charCount, int totalCount)
            {
                if (charCount != groupCount && groupCount == totalCount)
                    return totalCount;
                return groupCount - 2 * charCount + totalCount;
            }

        }

        private static bool IsCharVowel(this char c)
        {
            return c.Equals('A') || c.Equals('E') || c.Equals('I') || c.Equals('O') || c.Equals('U');
        }
    }
}