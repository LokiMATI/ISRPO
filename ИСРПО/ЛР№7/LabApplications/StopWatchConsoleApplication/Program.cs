using System.Diagnostics;

Stopwatch totalStopWatch = Stopwatch.StartNew();

Stopwatch stopWatch = new();

stopWatch.Start();
ulong result = Factorial(120);
stopWatch.Stop();
var firstTs = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение первого факториала: {firstTs.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=Factorial, Elapsed={firstTs.TotalMilliseconds} ms\n");


stopWatch.Restart();
result = Factorial(120);
stopWatch.Stop();
var secondTS = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение второго факториала: {secondTS.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=Factorial, Elapsed={secondTS.TotalMilliseconds} ms\n");

stopWatch.Restart();
result = Factorial(120);
stopWatch.Stop();
var thirdTS = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение третьего факториала: {thirdTS.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=Factorial, Elapsed={thirdTS.TotalMilliseconds} ms\n");

Debug.WriteLine($"Среднее время выполнения операций факториала: {(firstTs.TotalMilliseconds + secondTS.TotalMilliseconds + thirdTS.TotalMilliseconds) / 3} ms\n");


stopWatch.Restart();
var size = SumFilesSize(@"C:\temp");
stopWatch.Stop();
firstTs = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение первого подсчёта размера файлов в папке: {firstTs.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=SumFilesSize, Elapsed={firstTs.TotalMilliseconds} ms\n");

stopWatch.Restart();
size = SumFilesSize(@"C:\temp");
stopWatch.Stop();
secondTS = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение второго подсчёта размера файлов в папке: {secondTS.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=SumFilesSize, Elapsed={secondTS.TotalMilliseconds} ms\n");

stopWatch.Restart();
size = SumFilesSize(@"C:\temp");
stopWatch.Stop();
thirdTS = stopWatch.Elapsed;
Debug.WriteLine($"Время выполнение третьего подсчёта размера файлов в папке: {thirdTS.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=SumFilesSize, Elapsed={thirdTS.TotalMilliseconds} ms\n");

Debug.WriteLine($"Среднее время выполнения операций подсчёта размера файлов в папке: {(firstTs.TotalMilliseconds + secondTS.TotalMilliseconds + thirdTS.TotalMilliseconds) / 3} ms");

totalStopWatch.Stop();
var totalTs = totalStopWatch.Elapsed;
Debug.WriteLine($"Общее время выполнения всех операций: {totalTs.TotalMilliseconds} ms");
File.AppendAllText("timings.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Operation=Main, Elapsed={totalTs.TotalMilliseconds} ms\n");

static ulong Factorial(ulong x)
{
    if (x == 1) 
        return 1;
    return x * Factorial(x - 1);
}

static long SumFilesSize(string directoryName)
{
    long result = 0;
    DirectoryInfo dir = new(directoryName);

    var directories = dir.GetDirectories();

    foreach (var directory in directories)
        result += SumFilesSize(directory.FullName);

    var files = dir.GetFiles();
    result = files.Sum(f => f.Length);

    return result;
}