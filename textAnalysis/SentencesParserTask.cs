using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var textList = new List<List<string>>();
            var sentencesList = new List<string>();

            var sentences = text.Split('.', '!', '?', ';', ':', '(', ')');
            string[] stringSeparators = new string[]
            { " ", ":", "-", ",", "\t", "\r", "\n", "^", "#", "$", "+", "=", "1", "\"", "—", "…" };

            SplitSentence(sentences, stringSeparators, textList);

            return textList;
        }

        public static void SplitSentence(string[] sentences, string[] sentencesSeparators, List<List<string>> textList)
        {
            StringBuilder correctWord = new StringBuilder();
            foreach (var moreWord in sentences)
            {
                var words = moreWord.Split(sentencesSeparators, StringSplitOptions.RemoveEmptyEntries);
                var wordList = new List<string>();
                foreach (var oneWord in words)
                {
                    correctWord.Clear();
                    foreach (var simbol in oneWord)
                    {
                        if (char.IsLetter(simbol) || simbol == '\'') correctWord.Append(simbol);
                    }
                    if (correctWord.Length > 0) wordList.Add(correctWord.ToString().ToLower());
                }
                if (wordList.Count > 0) textList.Add(wordList);
            }
        }
    }
}