using System.Diagnostics;

#region Task 1-2
Console.WriteLine("Для выхода необходимов ввести 'exit'\n");
while (true)
{
    Console.Write("Введите первое слагаемое: ");
    Debug.WriteLine("Debug: Вводится первое слагаемое");
    Trace.WriteLine("Trace: Вводится первое слагаемое");
    string input = Console.ReadLine();
    if (input == "exit")
        break;
    var firstNumber = int.Parse(input);

    Console.Write("Введите второе слагаемое: ");
    Debug.WriteLine("Debug: Вводится второе слагаемое");
    Trace.WriteLine("Trace: Вводится второе слагаемое");
    input = Console.ReadLine();
    if (input == "exit")
        break;
    var secondNumber = int.Parse(input);

    Debug.WriteLine("Debug: Производится сложение слагаемых");
    Trace.WriteLine("Trace: Производится сложение слагаемых");
    var sum = firstNumber + secondNumber;
    Debug.WriteLine("Выводится сумма");
    Console.WriteLine($"Сумма: {sum}\n");
}
#endregion

#region Task 3
Console.WriteLine(CalculateDiscount(100, 0.5));
Console.WriteLine(CalculateDiscount(0, 0.5));
Console.WriteLine(CalculateDiscount(-100, 0.5));
Console.WriteLine(CalculateDiscount(100, 1.0));
Console.WriteLine(CalculateDiscount(100, 0));
Console.WriteLine(CalculateDiscount(100, -0.1));
Console.WriteLine(CalculateDiscount(100, 1.1));

static double CalculateDiscount(double price, double discountRate)
{
    Debug.Assert(price > 0, "Цена должна быть больше нуля.");
    Debug.Assert(0 <= discountRate & discountRate <= 1, "Параметр discountRate должен находится в диапозоне от 0 до 1.");

    var result = price * discountRate;
    Debug.Assert(result <= price, "Возвращаемое значение всегда меньше или равен price.");

    return result;
}
#endregion

#region Task 4
MainMethod();
static void MainMethod() 
{
    Console.WriteLine("Вызван метод 'MainMethod'");
    MethodA();
}

static void MethodA()
{
    Console.WriteLine("Вызван метод 'MethodA'");
    MethodB();
}

static void MethodB()
{
    Console.WriteLine("Вызван метод 'MethodB'");
    MethodC();
}

static void MethodC()
{
    Console.WriteLine("Вызван метод 'MethodC'");
    try
    {
        throw new DivideByZeroException();
    }
    catch (Exception ex)
    {
        File.WriteAllText("StackTrace.txt", ex.StackTrace);
    }
}
#endregion