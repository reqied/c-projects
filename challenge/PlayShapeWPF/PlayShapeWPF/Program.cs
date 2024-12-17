using System;
using System.Threading;
using System.Windows;
using ShapeWPF;

namespace ConsoleApp
{
    internal class Program
    {
        [STAThread] // Требуется для запуска WPF
        static void Main(string[] args)
        {
            // Пример входной строки
            string input = "figure1=ellipse,rectangle,triangle|(1,1) (2,2) (3,3)";

            // Создаём поток для запуска WPF
            string result = null;
            var thread = new Thread(() =>
            {
                var app = new Application();
                var mainWindow = new MainWindow();
                mainWindow.Show();
                result = mainWindow.ProcessInput(input); // Передаём данные в WPF
                app.Run(); // Запуск приложения
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join(); // Ожидаем завершения

            // Вывод результата в консоль
            Console.WriteLine("Result from WPF: " + result);
        }
    }
}