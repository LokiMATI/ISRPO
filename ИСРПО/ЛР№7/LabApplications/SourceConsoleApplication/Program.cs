using System.Diagnostics;
using TraceSourceConsoleApplication;

SourceSwitch sourceSwitch = new("StorageSwitch")
{
    Level = SourceLevels.Off
};

TraceSource ts = new("Storage")
{
    Switch = sourceSwitch
};

ts.Listeners.Clear();
ts.Listeners.Add(new TextWriterTraceListener("storage.log", "fileListener"));
Trace.AutoFlush = true;

ts.TraceEvent(TraceEventType.Information, 0, "Производится загрузка пользователей.");
List<User> users = new()
{
    new()
    {
        Id = 0,
        Name = "root",
        Password = "root",
    }
};
try
{
    ts.TraceEvent(TraceEventType.Information, 1, "Загрузка пользователей окончена.");
    Console.WriteLine("Список пользователей:");
    users.ForEach(u => Console.WriteLine($"{u.Id} | {u.Name} | {u.Password}"));

    ts.TraceEvent(TraceEventType.Information, 2, "Ввод идентификатора нового пользователя.");
    ts.TraceEvent(TraceEventType.Warning, 2, "Идентификатор не должен быть пустым и должен представлять целое число.");
    Console.Write("Введите идентификатор нового пользователя: ");
    var id = int.Parse(Console.ReadLine());
    ts.TraceEvent(TraceEventType.Verbose, 2, $"Значение идентификатора: {id}");

    ts.TraceEvent(TraceEventType.Information, 3, "Ввод имени нового пользователя.");
    ts.TraceEvent(TraceEventType.Warning, 3, "Имя не должен быть пустым.");
    Console.Write("Введите имя нового пользователя: ");
    var name = Console.ReadLine();
    ts.TraceEvent(TraceEventType.Verbose, 3, $"Значение имени: {name}");
    if (string.IsNullOrWhiteSpace(name))
        throw new NullReferenceException("Имя не может быть пустым.");

    ts.TraceEvent(TraceEventType.Information, 4, "Ввод пароля нового пользователя.");
    ts.TraceEvent(TraceEventType.Warning, 4, "Пароль не должен быть пустым.");
    Console.Write("Введтие пароль пользователя: ");
    var password = Console.ReadLine();
    ts.TraceEvent(TraceEventType.Verbose, 4, $"Значение пароля: {password}");
    if (string.IsNullOrWhiteSpace(name))
        throw new NullReferenceException("Пароль не может быть пустым.");

    User newUser = new()
    {
        Id = id,
        Name = name,
        Password = password
    };

    ts.TraceEvent(TraceEventType.Information, 5, "Добавление нового пользователя в хранилище.");
    users.Add(newUser);
    Console.WriteLine("\nСписок пользователей:");
    users.ForEach(u => Console.WriteLine($"{u.Id} | {u.Name} | {u.Password}"));

    ts.TraceEvent(TraceEventType.Information, 6, "Удаления пользователя по идентификатору из хранилища.");
    ts.TraceEvent(TraceEventType.Warning, 6, "Идентификатор не должен быть пустым и должен представлять целое число. Должен быть указан идентификатор существующего пользователя.");
    Console.Write("\nВведите идентификатор пользователя для удаления: ");
    id = int.Parse(Console.ReadLine());
    ts.TraceEvent(TraceEventType.Verbose, 6, $"Значение идентификатора: {id}");

    if (users.Any(u => u.Id == id))
    {
        users.Remove(users.First(u => u.Id == id));
        ts.TraceEvent(TraceEventType.Information, 7, "Удалён пользователь по идентификатору из хранилища.");
    }
    else
        throw new ArgumentException("ольователя с таким идентификатором нет.");

    Console.WriteLine("Список пользователей:");
    users.ForEach(u => Console.WriteLine($"{u.Id} | {u.Name} | {u.Password}"));
}
catch (Exception ex)
{
    ts.TraceEvent(TraceEventType.Error, -1, $"Ошибка: {ex.Message}");
}
