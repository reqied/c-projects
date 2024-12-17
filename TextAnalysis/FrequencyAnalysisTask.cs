namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var dictionaryOfAllVar = new Dictionary<string, Dictionary<string, int>>();
        foreach (var sentence in text)
        {
            dictionaryOfAllVar = CreateNGrammsCounter(dictionaryOfAllVar, sentence, 1);
            dictionaryOfAllVar = CreateNGrammsCounter(dictionaryOfAllVar, sentence, 2);
        }
        var result = new Dictionary<string, string>();
        foreach (var dictionaryForOneElem in dictionaryOfAllVar)
        {
            var value = dictionaryForOneElem.Key;
            var dictionaryWordPopularity = dictionaryForOneElem.Value;
            if (dictionaryWordPopularity.Count == 0)
            {
                continue;
            }
            var maximum = dictionaryWordPopularity.Values.Max();
            var frequentKeys = dictionaryWordPopularity.Where(x => x.Value == maximum).Select(x => x.Key).ToList();
            result.Add(value, CompareStringAndReturnMax(frequentKeys));
        }
        return result;
    }

    public static string CompareStringAndReturnMax(List<string> frequentKeys)
    {
        var maxString = frequentKeys[0];
        for (var k = 0; k < frequentKeys.Count - 1; k++)
        {
            var t = string.CompareOrdinal(maxString, frequentKeys[k + 1]);
            maxString = t < 0 ? maxString: frequentKeys[k + 1]; 
        }
        return maxString;
    }
    
    public static Dictionary<string, Dictionary<string, int>> CreateNGrammsCounter(
        Dictionary<string, Dictionary<string, int>> dictionaryOfAllVar,
        List<string> sentence, 
        int step)
    {
        for (var i = 0; i < sentence.Count - step; i++)
            {
                var word = step == 1 ? sentence[i] : sentence[i] + " " + sentence[i + 1];
                var indexedWord = sentence[i + step];
                if (dictionaryOfAllVar.TryAdd(word, new Dictionary<string, int>()))
                {
                    dictionaryOfAllVar[word].Add(indexedWord, 1);
                }
                else
                {
                    if (!dictionaryOfAllVar[word].TryAdd(indexedWord, 1))
                    {
                        dictionaryOfAllVar[word][indexedWord]++;
                    }
                }
            }
        return dictionaryOfAllVar;
    }
}