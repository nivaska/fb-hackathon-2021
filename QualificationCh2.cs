using System;
using System.Collections.Generic;
using System.Linq;
using fb_hacker_cup_2021.helpers;

namespace fb_hacker_cup_2021
{
    internal static class Qualification_ch2
    {
        // Qualification - Task 2 - String Consistency
        public static void Run()
        {
            var inputFilePath = "./consistency_chapter_2_input.txt";
            var fileHelper = new FileHelper();
            var inputData = fileHelper.ReadFile(inputFilePath);
            var inputCount = int.Parse(inputData[0]);

            var outputData = new List<string>();
            var numOfRepl = 0;
            var inputPtr = 1;
            for (int i = 1; i <= inputCount; i++)
            {
                inputPtr = inputPtr + numOfRepl;
                var str = inputData[inputPtr++];
                numOfRepl = int.Parse(inputData[inputPtr++]);

                var timeTaken = 0;
                if (IsStringConsistent(str))
                {
                    timeTaken = 0;
                }
                else
                {
                    var replacements = inputData.Skip(inputPtr).Take(numOfRepl).ToList();
                    var graph = new Graph(replacements);
                    var possibleDestinations = replacements.Select(s => s[1]).Distinct().ToList();
                    var letters = str.GroupBy(x => x).Select(g => new {Letter = g.Key, Count = g.Count()});

                    var times = new List<int>();
                    foreach(var destination in possibleDestinations){

                        var timeForDest = 0;
                        foreach(var letter in letters.Where(l => !l.Letter.Equals(destination))){
                            var minDist = graph.GetMinDistance(letter.Letter, destination);
                            if(minDist == -1){
                                timeForDest = -1;
                                break;
                            }

                            timeForDest += minDist * letter.Count;
                        }
                        times.Add(timeForDest);
                    }

                    timeTaken = times.Where(t => t > -1).Count() > 0?
                                times.Where(t => t > -1).Min():
                                -1;

                }

                outputData.Add($"Case #{i}: {timeTaken}");
            }

            fileHelper.WriteToFile(outputData, "out_qual_a2.txt");
        }

        private static bool IsStringConsistent(string s)
        {
            return s.Distinct().Count() == 1;
        }

    }

    internal class Graph
    {
        Dictionary<char, GraphNode> network;

        public Graph(List<string> connections)
        {
            network = new Dictionary<char, GraphNode>();

            foreach (var connection in connections)
            {
                ConnectNodes(connection[0], connection[1]);
            }
        }

        private void ConnectNodes(char source, char destination)
        {
            if (!network.ContainsKey(source)) network.Add(source, new GraphNode(source));
            if (!network.ContainsKey(destination)) network.Add(destination, new GraphNode(destination));

            network[source].AddSuccessor(network[destination]);
        }

        public int GetMinDistance(char source, char destination)
        {
            if (!network.ContainsKey(source) || !network.ContainsKey(destination))
                return -1;

            return network[source].GetMinDistance(destination, new List<char>());
        }

    }

    internal class GraphNode
    {
        readonly char _letter;
        List<GraphNode> _successorNodes;

        public GraphNode(char letter)
        {
            _letter = letter;
            _successorNodes = new List<GraphNode>();
        }

        public char Letter
        {
            get
            {
                return _letter;
            }
        }

        public void AddSuccessor(GraphNode node)
        {
            _successorNodes.Add(node);
        }

        public int GetMinDistance(char destination, List<char> path)
        {
            if (_successorNodes.Count == 0 || path.Contains(_letter))
                return -1;

            if (_successorNodes.Where(n => n.Letter.Equals(destination)).Count() >= 1)
                return 1;

            var distancesFromSuccessors = new List<int>();
            path.Add(_letter);

            foreach (var succesor in _successorNodes)
            {
                var dist = succesor.GetMinDistance(destination, path);
                if (dist != -1)
                {
                    distancesFromSuccessors.Add(dist + 1);
                }
            }

            return distancesFromSuccessors.Count != 0 ? distancesFromSuccessors.Min() : -1;
        }

        public override string ToString()
        {
            return $"{_letter} -> {String.Join(',', _successorNodes?.Select(n => n._letter))}";
        }
    }
}