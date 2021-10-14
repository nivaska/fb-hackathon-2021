using fb_hacker_cup_2021.helpers;
using System.Collections.Generic;

namespace fb_hacker_cup_2021
{
    internal class Round1_A1
    {

        public static void Run(string inputFilePath)
        {

            var fileHelper = new FileHelper();
            var inputData = fileHelper.ReadFile(inputFilePath);
            var T = int.Parse(inputData[0]);

            var outputData = new List<string>();
            for (int i = 1; i <= T; i ++)
            {
                var N = int.Parse(inputData[(i * 2)-1]);
                var word = inputData[i*2];

                char lastLetter = '0';
                int handSwaps = 0;
                for(int j = 0; j < N; j++ )
                {
                    if(word[j] != 'F' && lastLetter != word[j])
                    {
                        lastLetter = word[j];
                        handSwaps++;
                    }
                }

                handSwaps = handSwaps != 0 ? handSwaps - 1 : 0;

                outputData.Add($"Case #{i}: {handSwaps}");
            }

            fileHelper.WriteToFile(outputData, "out_round1_a1.txt");
        }

    }
}
