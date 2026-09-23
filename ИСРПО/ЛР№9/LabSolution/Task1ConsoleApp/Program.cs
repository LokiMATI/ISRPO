string filePath = "data.txt";
ReadFromFile(filePath);

async Task ReadFromFile(string filePath)
{
    if (!File.Exists(filePath))
    {
        Console.WriteLine("File not found.");
        return;
    }

    using var reader = new StreamReader(filePath, null, false, 8192);
    string? line;
    while ((line = await reader.ReadLineAsync()) != null)
        Console.WriteLine(line);
}