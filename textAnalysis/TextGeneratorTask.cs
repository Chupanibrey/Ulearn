using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var finalPhrase = phraseBeginning;
            for(int i = 0; i < wordsCount; i++)
            {
                var words = finalPhrase.Split(' ');
                var wordsLength = words.Length;
                if (wordsLength >= 2 && nextWords.ContainsKey(words[wordsLength - 2] + " " + words[wordsLength - 1]))
                {
                    finalPhrase += " " + nextWords[words[wordsLength - 2] + " " + words[wordsLength - 1]];
                }
                else if (wordsLength >= 1 && nextWords.ContainsKey(words[wordsLength - 1]))
                {
                    finalPhrase += " " + nextWords[words[wordsLength - 1]];
                }
                else break;
            }
            return finalPhrase;
        }
    }
}