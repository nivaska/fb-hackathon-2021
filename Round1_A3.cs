using System.Collections.Generic;
using fb_hacker_cup_2021.helpers;

namespace fb_hacker_cup_2021
{
    internal class Round1_A3
    {

        public static void Run(string inputFilePath)
        {
            var fileHelper = new FileHelper();
            var inputData = fileHelper.ReadFile(inputFilePath);
            var T = int.Parse(inputData[0]);

            var outputData = new List<string>();
            for (int i = 1; i <= T; i++)
            {
                var word = inputData[i * 2];
                word = ExpandString(word);
                var N = word.Length;
                char lastLetter = '0';
                long handSwaps = 0;

                int start = 0;
                for (int j = 0; j < N; j++)
                {

                    if (word[j] != 'F')
                    {
                        if (lastLetter != '0' && word[j] != lastLetter)
                        {
                            long leftCombinations = start * (start + 1) / 2;
                            long rightCombinations = (N - j - 1) * (N - j) / 2;
                            long totalCombinations = (N - (j - start)) * (N - (j - start) + 1) / 2;

                            handSwaps += (totalCombinations - leftCombinations - rightCombinations);
                        }

                        lastLetter = word[j];
                        start = j;
                    }

                }

                long output = handSwaps % 1000000007;
                outputData.Add($"Case #{i}: {output}");
            }

            fileHelper.WriteToFile(outputData, "./../../../out_round1_a3.txt");
        }

        private static string ExpandString(string s)
        {
            var result = "";
            foreach (var c in s)
            {
                if (c != '.')
                {
                    result += c;
                } else
                {
                    result += result;
                }
            }

            return result;
        }

    }
}
