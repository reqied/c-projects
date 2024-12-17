using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace SypherLong;

public class Solver
{
    public static void Main()
    {
        var str = "#a first longest word of the message=everywhere# uzsf5qq5w smzszdz4ywuz4zsctvr0s7z0s4'3s5fsuz4sf'0cu#";
        var answer = FindRightSolution(str);
        Console.WriteLine(answer);
    }

    public static string FindRightSolution(string str)
    {
        var splitWithCage = str.Split('#', StringSplitOptions.RemoveEmptyEntries);
        var thatWord = splitWithCage[0].Split("=")[1];
        var cypher = splitWithCage[1];
        cypher = cypher.Replace(' ', 'ъ');
        var cypherBuilder = new StringBuilder(cypher);
        var potentialPhrase = new List<string>();
        var lastSplitChar = char.MaxValue;
        var thatWordCoded = "";
        
        for (var i = 0; i < cypher.Length; i++)
        {
            var currentSplitChar = cypherBuilder[i];
            var arrayOfPotentialWords = cypher.Split(currentSplitChar);

            var maxLength = arrayOfPotentialWords.Select(e => e.Length).Prepend(int.MinValue).Max();
            if (maxLength != thatWord.Length) continue;

            if (currentSplitChar == lastSplitChar) break;
            potentialPhrase.Add(string.Join(" ", arrayOfPotentialWords));
            lastSplitChar = currentSplitChar;
            thatWordCoded = FindThatWordCoded(arrayOfPotentialWords, maxLength, thatWordCoded);
        }

        foreach (var phrase in potentialPhrase)
        {
            var ans = GetAnswer(thatWord, phrase, thatWordCoded);
            if (ans == "0") continue;
            return ans;
        }
        return "шляпа";
    }

    private static string GetAnswer(string thatWord, string potentialPhrase, string thatWordCoded)
    {
        var readText = File.ReadAllLines(@"D:\work\SypherLong\SypherLong\HarryPotterText.txt");
        var previousLine = "";
        var nearlyAnswerPhrases = FiillNearlyAnswerPhrases(readText, thatWord, previousLine, potentialPhrase, thatWordCoded);

        var arrayOfPatterns = potentialPhrase.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var indexOfThatWordInPattern = Array.IndexOf(arrayOfPatterns, thatWordCoded);
        foreach (var a in nearlyAnswerPhrases)
        {
            var amountOfThatWordInPhrase = a.Split().GroupBy(x => x).Where(x => x.Key == thatWord).Select(x => x.Count()).FirstOrDefault();
            var arrayOfWords = a.Split(" ");
            var indexOfThatWord = Array.IndexOf(arrayOfWords, thatWord);
            var leftBorder = indexOfThatWord - indexOfThatWordInPattern - 1;
            var rightBorder =  indexOfThatWord - indexOfThatWordInPattern + arrayOfPatterns.Length;
            var answerPhrase2 = arrayOfWords.Where((t, i) => i > leftBorder && i < rightBorder).ToList();
            if (amountOfThatWordInPhrase > 1 && arrayOfWords.Length > amountOfThatWordInPhrase * 3)
            {
                var newRightBorder = rightBorder; 
                for(var q = 0; q < amountOfThatWordInPhrase-1; q++)
                {
                    if (newRightBorder < 0) break;
                    var arrayIfMoreThenOne = arrayOfWords[newRightBorder..^1];
                    var nextIndexOfThatWord = Array.IndexOf(arrayIfMoreThenOne, thatWord); 
                    newRightBorder = nextIndexOfThatWord - indexOfThatWordInPattern - 1;
                    var newLeftBorder =  nextIndexOfThatWord - indexOfThatWordInPattern + arrayOfPatterns.Length;
                    var answerPhrase1 = arrayIfMoreThenOne.Where((t, j) => j > newRightBorder && j < newLeftBorder).ToList();
                    if (CheckIfPhraseMatchPattern(arrayOfPatterns, answerPhrase1, out var answer1)) continue;
                    return answer1;
                }
            }
            if (CheckIfPhraseMatchPattern(arrayOfPatterns, answerPhrase2, out var answer2)) continue;
            return answer2;
        }

        return new string("0");
    }

    private static bool CheckIfPhraseMatchPattern(string[] arrayOfPatterns, List<string> answerPhrase1, out string answer1)
    {
        var flag1 = true;

        for (var i = 0; i < arrayOfPatterns.Length; i++)
        {
            if (answerPhrase1.Count < arrayOfPatterns.Length)
            {
                flag1 = false;
                break;
            }
            if (arrayOfPatterns[i].Length != answerPhrase1[i].Length) flag1 = false;
            if (!flag1) break;
        }
        if (!flag1)
        {
            answer1 = null;
            return true;
        }

        var answer = answerPhrase1.ToArray();
        answer1 = string.Join(" ", answer);
        return false;
    }

    private static string FindThatWordCoded(string[] arrayOfPotentialWords, int maxLength, string thatWordCoded)
    {
        foreach (var word in arrayOfPotentialWords)
            if (word.Length == maxLength)
            {
                thatWordCoded = word;
                break;
            }
        return thatWordCoded;
    }

    private static List<string> FiillNearlyAnswerPhrases(string[] readText, string thatWord, string prevLine, string potentialPhrase, string thatWordCoded)
    {
        var nearlyAnswerPhrases = new List<string>();
        var flag = false;
        var b = new StringBuilder();
        foreach (var phrase in readText)
        {
            var lowerPhrase = phrase.ToLower();
            if (lowerPhrase.Contains(thatWord))
            {
                var z = 0;
            }
            if (flag)
            {
                b.Append(lowerPhrase);
                var an = CleanTheString(b.ToString());
                nearlyAnswerPhrases.Add(an);
                flag = false;
                b.Clear();
            }

            if (!lowerPhrase.Contains(thatWord) || lowerPhrase.Contains(thatWord + 's'))
            {
                prevLine = lowerPhrase;
                continue;
            }
            flag = true;
            b.Append(prevLine);
            b.Append(lowerPhrase);
            prevLine = lowerPhrase;
        }

        return nearlyAnswerPhrases;
    }
    private static string CleanTheString(string str)
    {
        var charSplit = new char[] {')', ';',' ', '\t', '\n', '\r', '.', ',', '\"', '!', '?', '-', '”', '“', '—', '‘', '…'};
        var temp = str.ToLower().Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
        return string.Join(" ", temp);
    }
}