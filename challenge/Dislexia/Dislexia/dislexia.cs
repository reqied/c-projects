
namespace Dislexia;
class Program
{
    public static string Do(string input)
    {
        var filePath = @"D:\work\Dislexia\HarryPotterText.txt";
        var correctWords = ReadDictionaryFromFile(filePath);
        var result = input.Split("#")[2];
        var correctedText = CorrectText(result, correctWords);
        return correctedText;
    }
    
/// <summary>
    /// Читает слова из файла и возвращает их в виде массива строк
    /// </summary>
    static string[] ReadDictionaryFromFile(string filePath)
    {
        var text = File.ReadAllText(filePath);
        return text.Split(new[] { ' ', '\n', '\r', '.', ',', ';', '!', '?', '-', '_', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(word => word.ToLower())
                   .Distinct()
                   .ToArray();
    }

    /// <summary>
    /// Исправляет строку, используя словарь правильных слов.
    /// </summary>
    static string CorrectText(string input, string[] dictionary)
    {
        string?[] words = input.Split(new[] { ' ', '.', ',', ';', '!', '?', '-', '_', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < words.Length; i++)
        {
            var word = words[i].ToLower();

            if (!dictionary.Contains(word))
            {
                words[i] = FindClosestWord(word, dictionary);
            }
        }
        return string.Join(" ", words);
    }

    /// <summary>
    /// Находит слово той же длины и с такими же буквами в словаре.
    /// </summary>
    static string? FindClosestWord(string word, string[] dictionary)
    {
        var sortedWord = String.Concat(word.OrderBy(c => c));
    
        return dictionary
            .FirstOrDefault(d => d.Length == word.Length && String.Concat(d.OrderBy(c => c)) == sortedWord);
    }
}
