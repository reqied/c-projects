using System;
using System.Data;
using System.Text;
using Microsoft.VisualBasic;

namespace CyclePractice
{
    public static class Pracrice
    {

        public static void Smth()
        {
            var A = 7;
            var B = 5;
            int[] numberArray = {4, 1};
            var numInDecimal = 0;
            for (var i = 0; i < numberArray.Length; i++)
            {
                numInDecimal += numberArray[i] * (int)Math.Pow(A, numberArray.Length - i - 1);
            }
            string numInB = "";
            while (numInDecimal >= B)
            {
                numInB += (numInDecimal % B).ToString();
                numInDecimal /= B;
            }
            numInB += (numInDecimal % B).ToString();
            Console.WriteLine(string.Join("", numInB.ToCharArray().Reverse()));
        }

        private static string DecodeMessage(string[] lines)
        {
            var rightWords = new List<string>();
            foreach (var line in lines)
            {
                var li = line.Split(" ");
                foreach (var i in li)
                {
                    if (i.Length > 0 && char.IsUpper(i, 0))
                    {
                        rightWords.Add(i);
                    }
                }
            }
            rightWords.Reverse();
            var someOut = new string[rightWords.Count];
            for (var i = 0; i < rightWords.Count; i++)
            {
                someOut[i] = rightWords[i];
            }
            return string.Join("", someOut);
        }

        private static Dictionary<string, List<string>> OptimizeContacts(List<string> contacts)
        {
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var contact in contacts)
            {
                var name = contact.Substring(0, 2).Replace(":", "");
                if (!dictionary.ContainsKey(name))
                {
                    dictionary[name] = new List<string>();
                    dictionary[name].Add(contact.Replace(":", ""));
                }
            }
            return dictionary;
        }

        public static void Main()
        {
            Console.WriteLine(GetBenfordStatistics("1"));
            Console.WriteLine(GetBenfordStatistics("abc"));
            Console.WriteLine(GetBenfordStatistics("123 456 789"));
            Console.WriteLine(GetBenfordStatistics("abc 123 def 456 gf 789 i"));
            Console.WriteLine(GetBenfordStatistics("Burj Khalifa 830"));
            var word = "гога";
            var builderWord = new StringBuilder(word);
            Console.WriteLine(builderWord);
        }
        public static int[] GetBenfordStatistics(string text)
        {
            var statistics = new int[10];
            var TextArray = text.Split(" ");
            var DigitsArray = new List<string>();
            foreach (var str in TextArray)
            {
                if (CheckIfDigit(str[0].ToString()))
                {
                    var a = int.Parse(str[0].ToString());
                    statistics[a]++;
                }
            }
            return statistics;
        }

        public static bool CheckIfDigit(string s)
        {
            var success = int.TryParse(s, out var number);
            return success;
        }

        public static string ReplaceIncorrectSeparators(string text)
        {
            char[] delimiterChars = { ' ', ',', ':', ';', '-'};
            var words = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("/t", text);
        }

        private static string ApplyCommands(string[] commands)
        {
            var builder = new StringBuilder();
            foreach(var command in commands)
            {
                if (command.StartsWith("push"))
                {
                    builder.Append(command[5..]);
                }
                else
                {
                    int pop = int.Parse(command.Substring(4));
                    builder.Remove(pop <= builder.Length ? builder.Length - pop : 0, pop);
                }
            }
            return builder.ToString();
        }
    }
}
