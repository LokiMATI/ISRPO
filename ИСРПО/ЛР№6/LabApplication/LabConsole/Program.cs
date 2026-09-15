using NLog;
Logger logger = LogManager.GetCurrentClassLogger();

try
{
    Console.Write("Введите значение для числителя: ");
    var numerator = int.Parse(Console.ReadLine());
    Console.Write("Введите значение для знаменателя: ");
    var denominator = int.Parse(Console.ReadLine());

    if (denominator == 0)
        throw new DivideByZeroException("Попытка деления вещественного числа на ноль.");

    double result = numerator / denominator;
    Console.WriteLine($"Результат: {result}");
}
catch (FormatException ex)
{
    logger.Error(ex, "Ошибка парсинга: введенный текст не является числом.");
}
catch (DivideByZeroException ex)
{
    logger.Error(ex, "Ошибка: Делить на 0 нельзя.");
}
catch (Exception ex)
{
    logger.Error(ex, "Возникла непредвиденная ошибка.");
}

LogManager.Shutdown();

