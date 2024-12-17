using Microsoft.Win32.SafeHandles;

namespace TextAnalysis;

static class PrevVersion
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var allOfBigramms = new Dictionary<string, int>();
        var allOfThreegramms = new Dictionary<string, int>();
        foreach (var wordList in text)
        {
            var biGramms = MakeNGrammsDictionary(wordList, wordList.Count - 1, false);
            var threeGramms = MakeNGrammsDictionary(wordList, wordList.Count - 2, true);
            allOfBigramms = allOfBigramms.Concat(biGramms).ToDictionary(x=>x.Key, x=>x.Value);
            allOfThreegramms = allOfThreegramms.Concat(threeGramms).ToDictionary(x=>x.Key, x=>x.Value);
        }
        allOfBigramms = (Dictionary<string, int>)allOfBigramms.OrderByDescending(pair => pair.Value);
        allOfThreegramms = (Dictionary<string, int>)allOfThreegramms.OrderByDescending(pair => pair.Value);
        return result;
    }

    public static Dictionary<string, int> MakeNGrammsDictionary(List<string> wordList, int stop, bool ThreeGramOrNot)
    {
        var nGramms = new Dictionary<string, int>();
        var cycleStep = ThreeGramOrNot ? 3 : 2;
        for (var i = 0; i < stop; i++)
            {
                var subList = wordList.Where(elem => wordList.IndexOf(elem) >= i && wordList.IndexOf(elem) < i + cycleStep).ToList();
                var nGramm = string.Join(" ", subList);
                if(!nGramms.TryAdd(nGramm, 1))
                {
                    nGramms[nGramm]++;
                }
            }
        return nGramms;
    }

}