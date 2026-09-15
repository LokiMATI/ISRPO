using UserExceptionConsoleApp;

try
{
    Console.Write("\nВведтие возраст: ");
    var age = int.Parse(Console.ReadLine());

    if (age < 0)
        throw new NegativeNumberException("Ошибка: Возраст не может быть меньше 0");
}
catch (NegativeNumberException ex)
{
    Console.WriteLine($"Пользовательское исключение: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}