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
    Console.WriteLine(ex.Message);
    File.AppendAllText("log.txt", $"[{DateTime.Now}] {ex.GetType()}: {ex.ToString()}\n");
}
catch (OverflowException ex)
{
    Console.WriteLine(ex.Message);
    File.AppendAllText("log.txt", $"[{DateTime.Now}] {ex.GetType()}: {ex.ToString()}\n");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    File.AppendAllText("log.txt", $"[{DateTime.Now}] {ex.GetType()}: {ex.ToString()}\n");
}
