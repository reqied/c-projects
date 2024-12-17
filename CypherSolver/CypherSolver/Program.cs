using System.Text;
using System;
using System.Threading;
using System.Linq.Expressions;


internal class CypherSolver
{
    private static void Main() //TaskResponse taskResponse
    {
        Console.WriteLine(5/4);
        //var input = taskResponse.ToString();
        var input = "#Caesar's code=8#pmhnw2vlhpq0hqv3q0qjqtq16hktwishnwtlmlhvmi1t6h2vlmzvmi1p#";
        var decryptedMessage = DecryptMessage(input);

        Console.WriteLine("Расшифрованное сообщение:");
        Console.WriteLine(decryptedMessage);
        //Console.WriteLine("makez176zaz6abb zca6z5li0ki0gza4170dz6hezc140e4za6" == decryptedMessage);
    }

    private static (string message, int shift) InputParse(string input)
    {

        var builder = new StringBuilder(input);
        builder.Remove(0, 1);
        builder.Remove(input.Length - 2, 1);
        var temp = builder.ToString().Split('#');
        var encryptedMessage = temp[1];
        var shiftBuilder = new StringBuilder();
        for (var i = 0; i < temp[0].Length; i++)
        {
            var symbol = temp[0][i];
            if (char.IsDigit(symbol))
            {
                shiftBuilder.Append(symbol);
            }
        }

        var shiftString = shiftBuilder.ToString();
        var shift = int.Parse(shiftString);
        return (encryptedMessage, shift);
    }
    private static string DecryptMessage(string input)
    {
        var messageShiftPair = InputParse(input);
        var message = messageShiftPair.message;
        var shift = messageShiftPair.shift;
        // Поскольку 30 мод 26 = 4, используем 4
        shift %= 26;

        var decrypted = new StringBuilder();

        foreach (var c in message)
            if (char.IsLower(c)) // Если символ - строчная буква
            {
                var decryptedChar = (char) (c - shift);
                // Обрабатываем переполнение
                if ('a' - decryptedChar == 1) decryptedChar += ' ';
                else if (decryptedChar < 'a') decryptedChar += (char) 26;// Перепрыгивает к концу алфавита
                    decrypted.Append(decryptedChar);
                
            }
            else if (char.IsDigit(c) || c == ' ' || c == '\'') // Не изменяем цифры, пробелы и апостроф
            {
                decrypted.Append(c);
            }

        return decrypted.ToString();
    }
}