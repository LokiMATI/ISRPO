using LabConsole;
using NLog;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.Marshalling;
Logger logger = LogManager.GetCurrentClassLogger();
AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    var ex = e.ExceptionObject as Exception;

    logger.Fatal(ex, "Глобальный обработчик исключений поймал ошибку: ");
    Console.WriteLine("Произошла ошибка, Подробности в логах");
}
ThreadPool.QueueUserWorkItem(_ =>
{
    throw new InvalidOperationException("Тестовая критическая ошибка для логера!");
});


//try
//{
//    Console.Write("Введите значение для числителя: ");
//    var numerator = int.Parse(Console.ReadLine());
//    Console.Write("Введите значение для знаменателя: ");
//    var denominator = int.Parse(Console.ReadLine());

//    if (denominator == 0)
//        throw new DivideByZeroException("Попытка деления вещественного числа на ноль.");

//    double result = numerator / denominator;
//    Console.WriteLine($"Результат: {result}");
//}
//catch (FormatException ex)
//{
//    logger.Error(ex, "Ошибка парсинга: введенный текст не является числом.");
//}
//catch (DivideByZeroException ex)
//{
//    logger.Error(ex, "Ошибка: Делить на 0 нельзя.");
//}
//catch (Exception ex)
//{
//    logger.Error(ex, "Возникла непредвиденная ошибка.");
//}

//try
//{
//    Console.Write("\nВведтие возраст: ");
//    var age = int.Parse(Console.ReadLine());

//    if (age < 0)
//        throw new NegativeNumberException("Ошибка: Возраст не может быть меньше 0");
//}
//catch (NegativeNumberException ex)
//{
//    logger.Error(ex);
//}
//catch (Exception ex)
//{
//    logger.Error(ex, "Возникла необрабатываемая ошибка");
//}

try
{
    using var file = new StreamReader(@"C:\temp\ispp31\ИСРПО\ЛР№6\Task3.txt");
    var text = file.ReadToEnd();

    int count = 0;
    foreach (var ch in text)
    {
        if (char.IsDigit(ch))
        {
            var number = Convert.ToInt32(ch);
            if (number % 2 == 0)
                count++;
        }
    }
    Console.WriteLine(count);
}
catch (FileNotFoundException ex)
{
    logger.Error(ex, "Файл не обнаружен");
}

LogManager.Shutdown();

