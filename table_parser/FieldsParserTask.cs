using System.Collections.Generic;
using NUnit.Framework;
using System.Text;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("", new string[0])] //��� �����
        [TestCase("a b", new[] { "a", "b" })] //������ ������ ����, ����������� ������ � ���� ������
        [TestCase("a  b", new[] { "a", "b" })] //����������� ������ >1 �������
        [TestCase(@"""'b'""", new[] { "'b'" })] //��������� ������� ������ �������
        [TestCase(@"'""a"" ""b""'", new[] { "\"a\" \"b\"" })] //������� ������� ������ ���������, ������ ������ �������
        [TestCase(@"''", new[] { "" })] //������ ����
        [TestCase(@"""a""b", new[] { "a", "b" })] //����������� ��� ��������, ������� ���� ����� ���� � ��������
        [TestCase(@"abc""def", new[] { "abc", "def" })] //��� ����������� �������, ���� � �������� ����� �������� ����
        [TestCase(@"""a \""b\""", new[] { @"a ""b""" })] //�������������� ������� ������� ������ �������
        [TestCase(@"""\\""", new[] { "\\" })]
        //�������������� �������� ���� ������ �������, �������������� �������� ���� ����� ����������� ��������
        [TestCase(@"'\'\''", new[] { @"''" })] // �������������� ��������� ������� ������ ���������
        [TestCase(@"  'a'", new[] { @"a" })] //������� � ������ ��� � ����� ������
        [TestCase(@"'a  ", new[] { @"a  " })] //������ � ����� ���� � ���������� ��������
        [TestCase("a \"bcd ef\" 'x y'", new[] { "a", "bcd ef", "x y" })]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static int LengthParse;

        public static List<Token> ParseLine(string line)
        {
            var listToken = new List<Token>();
            var i = 0;
            while (i < line.Length)
            {
                if (line[i] == ' ')
                {
                    i++;
                    continue;
                }
                else if (line[i] == '\'' || line[i] == '\"')
                {
                    listToken.Add(ReadQuotedField(line, i));
                    i += LengthParse;
                }
                else
                {
                    listToken.Add(ReadSimpleField(line, i));
                    i += LengthParse;
                }
            }
            return listToken;
        }

        public static Token ReadSimpleField(string line, int startIndex)
        {
            var token = new StringBuilder();
            var spaceChar = ' ';
            var i = startIndex;
            while (i < line.Length && line[i] != spaceChar
                && line[i] != '\'' && line[i] != '\"')
            {
                token.Append(line[i]);
                i++;
            }
            LengthParse = i - startIndex;
            return new Token(token.ToString(), startIndex, i - startIndex);
        }

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
            LengthParse = i - startIndex + 1;
            return new Token(token.ToString(), startIndex, i - startIndex + 1);
        }
    }
}