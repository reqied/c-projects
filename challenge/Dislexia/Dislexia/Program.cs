using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHunspell;

namespace cypherDislexya
{
    public class Program
    {
        readonly Hunspell _hunspell = new Hunspell("en_US.aff", "en_US.dic");

        private static string Parser(string word)
        {
            var cleanedString = word.Trim('#'); 
            var parts = cleanedString.Split('#');
            var result = parts[1];
            return result;
        }

        private static string DeCapitalize(string word)
        {
            var up = new StringBuilder();
            foreach (var letter in word)
            {
                up.Append(char.ToLower(letter));
            }

            return up.ToString();
        }

        private static string Capitalize(string word)
        {
            var up = new StringBuilder();
            foreach (var letter in word)
            {
                up.Append(char.ToUpper(letter));
            }

            return up.ToString();
        }

        public static void Main1()
        {
            var s = "#dyslexia#sanre fltwicik ms'tvue#";
            var instance = new Program(); 
            var result = Parser(s);
            var builder = new StringBuilder();
            foreach (var word in result.Split(' '))
            {
                if (!instance._hunspell.Spell(word))
                {
                    if (word.Contains('i'))
                    {
                        var newWord = Capitalize(word);
                        var fixedWord = DeCapitalize(instance.GetPermutations(newWord));
                        builder.Append(fixedWord);
                        builder.Append(' ');
                    }
                    else
                    {
                        var fixedWord = instance.GetPermutations(word);
                        builder.Append(fixedWord);
                        builder.Append(' ');
                    }
                }
                else
                {
                    builder.Append(word);
                    builder.Append(' ');
                }
            }

            Console.WriteLine(builder.ToString());
        }

        string GetPermutations(string word)
        {
            var s = new List<string>();   
            foreach (var chars in word.Permutations())
            {
                
                var permutation = new string(chars.ToArray());
                s.Add(permutation);
                if (_hunspell.Spell(permutation))
                {
                    Console.WriteLine(string.Join(" ", s));
                    return permutation;
                }
            }
            
            return null;
        }
    }

    public static class EnumerableExtensions
    {
        private static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            yield return item;

            foreach (var element in source)
                yield return element;
        }

        public static IEnumerable<IEnumerable<TSource>> Permutations<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var list = source.ToList();

            if (list.Count > 1)
                return from s in list
                       from p in Permutations(list.Take(list.IndexOf(s)).Concat(list.Skip(list.IndexOf(s) + 1)))
                       select p.Prepend(s);

            return new[] { list };
        }
    }
}