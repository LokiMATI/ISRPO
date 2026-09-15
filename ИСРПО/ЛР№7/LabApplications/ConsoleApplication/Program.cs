try
{
    Console.Write("Введтие первое целое число: ");
    var firstNumber = int.Parse(Console.ReadLine());

    Console.Write("Введтие второе целое число: ");
    var secondNumber = int.Parse(Console.ReadLine());

    Console.WriteLine($"Сложение: {firstNumber + secondNumber}");
    Console.WriteLine($"Вычитание: {firstNumber - secondNumber}");
    Console.WriteLine($"Уиножение: {firstNumber * secondNumber}");
    Console.WriteLine($"Деление: {firstNumber / secondNumber}");
}
catch (FormatException ex)
{
    LogException(ex);
}
catch (OverflowException ex)
{
    LogException(ex);
}
catch (Exception ex)
{
    LogException(ex);
}

static void LogException(Exception ex)
{
    Console.WriteLine(ex.Message);
    File.AppendAllText("log.txt", $"[{DateTime.Now}] {ex.GetType()}: {ex}\n");
}