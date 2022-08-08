using NUnit.Framework;
using System.Text;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var token = new StringBuilder();
            var startChar = line[startIndex];
            var i = startIndex + 1;
            while (i != line.Length && line[i] != startChar)
            {
                if (line[i] == '\\')
                {
                    token.Append(line[i + 1]);
                    i += 2;
                    continue;
                }
                token.Append(line[i]);
                i++;
            }
            if (i == line.Length) i -= 1;
            FieldsParserTask.LengthParse = i - startIndex + 1;
            return new Token(token.ToString(), startIndex, i - startIndex + 1);
        }
    }
}