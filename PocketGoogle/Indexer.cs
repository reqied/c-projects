using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> IndexToWord = new();
        private readonly Dictionary<int, HashSet<string>> DocumentWords = new(); 
        private readonly HashSet<char> Delimiters = new() { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };


        public void Add(int id, string document)
        {
            var wordsInDocument = new HashSet<string>();
            var position = 0;
            var words = document.Split(Delimiters.ToArray(), System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var wordStartIndex = document.IndexOf(word, position, StringComparison.Ordinal);
                if (!IndexToWord.ContainsKey(word))
                    IndexToWord[word] = new Dictionary<int, List<int>>();
                if (!IndexToWord[word].ContainsKey(id))
                    IndexToWord[word][id] = new List<int>();
                IndexToWord[word][id].Add(wordStartIndex);
                wordsInDocument.Add(word);
                position = wordStartIndex + word.Length;
            }
            if (!DocumentWords.ContainsKey(id))
                DocumentWords[id] = new HashSet<string>();
            foreach (var word in wordsInDocument)
            {
                DocumentWords[id].Add(word);
            }
        }



        public List<int> GetIds(string word)
        {
            if (IndexToWord.TryGetValue(word, out var value))
            {
                return new List<int>(value.Keys);
            }
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            var positions = new List<int>();
            if (IndexToWord.ContainsKey(word) && IndexToWord[word].ContainsKey(id))
            {
                positions.AddRange(IndexToWord[word][id]);
            }
            return positions;
        }

        public void Remove(int id)
        {
            if (DocumentWords.ContainsKey(id))
            {
                var wordsToRemove = DocumentWords[id];
                foreach (var word in wordsToRemove)
                {
					if (!IndexToWord.ContainsKey(word) || !IndexToWord[word].ContainsKey(id))
						continue;
                    IndexToWord[word].Remove(id);
                    if (IndexToWord[word].Count == 0)
                    {
                        IndexToWord.Remove(word);
                    }
                }
                DocumentWords.Remove(id);
            }
        }
    }
}