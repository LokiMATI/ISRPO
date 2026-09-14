using System.Diagnostics;
using TraceSourceConsoleApplication;

SourceSwitch sourceSwitch = new("StorageSwitch")
{
    Level = SourceLevels.Warning
};

TraceSource ts = new("Storage")
{
    Switch = sourceSwitch
};

ts.Listeners.Clear();
ts.Listeners.Add(new TextWriterTraceListener("storage.log", "fileListener"));
ts.Listeners.Add(new ConsoleTraceListener());
Trace.AutoFlush = true;

List<User> users = new()
{
    new()
    {
        Id = 0,
        Name = "root",
        Password = "root",
    }
};
Console.WriteLine("Список пользователей:");
users.ForEach(u => Console.WriteLine($"{u.Id} | {u.Name} | {u.Password}"));

Console.Write("Введите идентификатор пользователя: ");
var id = int.Parse(Console.ReadLine());

Console.Write("Введите имя пользователя: ");
var name = Console.ReadLine() ?? throw new NullReferenceException("Имя не может быть пустым.");

Console.Write("Введтие пароль пользователя: ");
var password =  Console.ReadLine() ?? throw new NullReferenceException("Пароль не может быть пустым.");

User newUser = new()
{
    Id = id,
    Name = name,
    Password = password
};

users.Add(newUser);

Console.Write("Введите идентификатор пользователя для удаления: ");
id = int.Parse(Console.ReadLine());