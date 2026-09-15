AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
{
    var ex = e.ExceptionObject as Exception;

    File.AppendAllText("crash.log", ex.ToString());
    Console.WriteLine("Произошла ошибка, Подробности в логах");
}

throw new InvalidOperationException("Тестовая критическая ошибка для логера!");
