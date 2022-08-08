using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var allAllgrams = GetBigrams(text);
            var allgrams = GetMostFrequentBigrams(allAllgrams);
            return allgrams;
        }

        public static Dictionary<string, Dictionary<string, int>> GetBigrams(List<List<string>> text)
        {
            var allBigrams = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < text.Count; i++)
            {
                for (int j = 0; j < text[i].Count - 1; j++)
                {
                    if (allBigrams.ContainsKey(text[i][j]))
                        if (allBigrams[text[i][j]].ContainsKey(text[i][j + 1]))
                            allBigrams[text[i][j]][text[i][j + 1]]++;
                        else allBigrams[text[i][j]].Add(text[i][j + 1], 1);
                    else
                    {
                        var temp = new Dictionary<string, int> { { text[i][j + 1], 1 } };
                        allBigrams.Add(text[i][j], temp);
                    }
                }
                if (text[i].Count > 2) AddTrigrams(text, allBigrams, i);
            }
            return allBigrams;
        }

        public static void AddTrigrams(List<List<string>> text,
                                       Dictionary<string, Dictionary<string, int>> allBigrams,
                                       int i)
        {
            for (int j = 0; j < text[i].Count - 2; j++)
            {
                if (allBigrams.ContainsKey(text[i][j] + " " + text[i][j + 1]))
                    if (allBigrams[text[i][j] + " " + text[i][j + 1]].ContainsKey(text[i][j + 2]))
                        allBigrams[text[i][j] + " " + text[i][j + 1]][text[i][j + 2]]++;
                    else allBigrams[text[i][j] + " " + text[i][j + 1]].Add(text[i][j + 2], 1);
                else
                {
                    var temp = new Dictionary<string, int> { { text[i][j + 2], 1 } };
                    allBigrams.Add(text[i][j] + " " + text[i][j + 1], temp);
                }
            }
        }

        public static Dictionary<string, string> GetMostFrequentBigrams(
            Dictionary<string, Dictionary<string, int>> allBigrams)
        {
            var mostFrequentBigrams = new Dictionary<string, string>();
            foreach (var word in allBigrams.Keys)
            {
                var pairs = new List<string>();
                int temp = int.MinValue;
                foreach (var pair in allBigrams[word].Keys)
                {
                    if (allBigrams[word][pair] > temp)
                    {
                        pairs.Clear();
                        pairs.Add(pair);
                        temp = allBigrams[word][pair];
                    }
                    else if (allBigrams[word][pair] == temp && string.CompareOrdinal(pair, pairs[0]) < 0)
                    {
                        pairs.Clear();
                        pairs.Add(pair);
                    }
                }
                mostFrequentBigrams.Add(word, pairs[0]);
            }
            return mostFrequentBigrams;
        }
    }
}