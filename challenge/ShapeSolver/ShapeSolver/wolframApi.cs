using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ShapeDetection
{
    class Program
    {
        private const string WolframApiKey = "RJ8W8A-UP37VQX8RK"; // Замените на ваш API-ключ

        static async Task Main(string[] args)
        {
            var points = 
            string.Join(",", "(294,167) (296,199) (293,176) (266,186) (252,185) (277,176) (289,192) (292,197) (253,192)".Split(' '));

            string shape = await GetShapeFromWolfram(points);
            Console.WriteLine($"Detected shape: {shape}");
        }

        public static async Task<string> GetShapeFromWolfram(string points)
        {
            // Форматируем точки в строку для запроса
            //string formattedPoints = FormatPoints(points);

            // Формируем запрос
            string query = $"{points}";

            // URL для API Wolfram Alpha
            string url = $"http://api.wolframalpha.com/v2/query?appid={WolframApiKey}&input={HttpUtility.UrlEncode(query)}&output=json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Читаем ответ от API
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Парсим JSON (можно использовать Newtonsoft.Json или System.Text.Json)
                    string shape = ParseShapeFromResponse(responseContent);

                    return shape;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return "Unknown";
                }
            }
        }

        private static string FormatPoints(List<(double X, double Y)> points)
        {
            List<string> pointStrings = new List<string>();
            foreach (var point in points)
            {
                pointStrings.Add($"({point.X}, {point.Y})");
            }
            return string.Join(", ", pointStrings);
        }

        private static string ParseShapeFromResponse(string jsonResponse)
        {
            // Пример: парсим JSON и извлекаем форму фигуры
            // Для этого используйте библиотеку Newtonsoft.Json или System.Text.Json
            // Пример упрощенной обработки (замените реальной логикой)
            if (jsonResponse.Contains("rectangle"))
                return "Rectangle";
            if (jsonResponse.Contains("ellipse"))
                return "Ellipse";
            if (jsonResponse.Contains("triangle"))
                return "Triangle";

            return "Unknown";
        }
    }
}
