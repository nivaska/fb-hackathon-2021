using fb_hacker_cup_2021.helpers;
using System;
using System.Collections.Generic;

namespace fb_hacker_cup_2021
{
    internal class Round1_A2
    {

        public static void Run(string inputFilePath)
        {

            var fileHelper = new FileHelper();
            var inputData = fileHelper.ReadFile(inputFilePath);
            var T = int.Parse(inputData[0]);

            var outputData = new List<string>();
            for (int i = 1; i <= T; i++)
            {
                var N = int.Parse(inputData[(i * 2) - 1]);
                var word = inputData[i * 2];

                char lastLetter = '0';
                long handSwaps = 0;

                int start = 0;
                for (int j = 0; j < N; j++)
                {

                    if (word[j] != 'F')
                    {
                        if(lastLetter!= '0' && word[j] != lastLetter)
                        {
                            long leftCombinations = start * (start + 1) /2;
                            long rightCombinations = (N - j - 1) * (N - j) / 2 ;
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

            fileHelper.WriteToFile(outputData, "out_round1_a2.txt");
        }

    }

    internal class SubstringCounter
    {
        private Char prevChar = '0';
        private int length;
        private int count = 0;


        public SubstringCounter(int len)
        {
            this.length = len;
        }

        public void CheckSwitch(char c, int index)
        {

            if (index % this.length != 1)
            {
                if(c!= 'F' && prevChar != '0' && c != prevChar)
                    count++;
            }

            if (c != 'F')
                prevChar = c;

            if (index % this.length == 0)
                prevChar = '0';
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public override string ToString()
        {
            return $"Length -> {this.length.ToString()}; Count -> {this.count.ToString()}";
        }
    }
}
