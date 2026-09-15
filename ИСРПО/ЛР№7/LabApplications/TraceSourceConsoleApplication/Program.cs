using System.Diagnostics;

TraceSource ts = new("Calculator")
{
    Switch = new("CalculatorSwitch") { Level = SourceLevels.Information }
};

ts.Listeners.Clear();
if (ts.Switch.Level == SourceLevels.Information)
    ts.Listeners.Add(new TextWriterTraceListener("trace.log", "fileListener"));
else if (ts.Switch.Level == SourceLevels.Verbose)
    ts.Listeners.Add(new TextWriterTraceListener("trace_verbose.log", "fileListener"));

ts.Listeners.Add(new ConsoleTraceListener());
Trace.AutoFlush = true;

ts.TraceEvent(TraceEventType.Start, 0, "Начало выполнения программы.");
try
{
    ts.TraceInformation("Начинается ввод значения первого целого числа.");
    ts.TraceEvent(TraceEventType.Warning, 1, "При вводе не целого числа будет вызвано исключение.");
    Console.Write("Введтие первое целое число: ");
    var firstNumber = int.Parse(Console.ReadLine());
    ts.TraceEvent(TraceEventType.Verbose, 1, $"Значение первого целого числа: {firstNumber}");

    ts.TraceInformation("Начинается ввод значения второго целого числа.");
    ts.TraceEvent(TraceEventType.Warning, 2, "При вводе не целого числа будет вызвано исключение.");
    Console.Write("Введтие второе целое число: ");
    var secondNumber = int.Parse(Console.ReadLine());
    ts.TraceEvent(TraceEventType.Verbose, 2, $"Значение второго целого числа: {secondNumber}.");

    ts.TraceInformation("Начало выполнения операции сложения.");
    var result = firstNumber + secondNumber;
    ts.TraceEvent(TraceEventType.Verbose, 3, $"Результат сложения: {result}.");

    ts.TraceInformation("Начало выполнения операции вычитания.");
    result = firstNumber - secondNumber;
    ts.TraceEvent(TraceEventType.Verbose, 4, $"Результат вычитания: {result}.");

    ts.TraceInformation("Начало выполнения операции умножения.");
    result = firstNumber * secondNumber;
    ts.TraceEvent(TraceEventType.Verbose, 5, $"Результат умножения: {result}.");

    ts.TraceInformation("Начало выполнения операции деления.");
    result = firstNumber / secondNumber;
    ts.TraceEvent(TraceEventType.Verbose, 6, $"Результат деления: {result}.");
}
catch (FormatException ex)
{
    ts.TraceEvent(TraceEventType.Error, -2, $"Неверный ввод значения целого числа: {ex}");
}
catch (OverflowException ex)
{
    ts.TraceEvent(TraceEventType.Error, -3, $"Переполнение стека: {ex}");
}
catch (Exception ex)
{
    ts.TraceEvent(TraceEventType.Error, -3, $"{ex.GetType}: {ex}");
}
ts.TraceEvent(TraceEventType.Stop, -1, "Конец выполнения программы.");

ts.Flush();
ts.Close();