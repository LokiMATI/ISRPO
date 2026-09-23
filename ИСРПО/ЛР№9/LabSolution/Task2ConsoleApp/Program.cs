string filePath = "data.txt";
WriteToFile(filePath, "Some data to be written to the file");

void WriteToFile(string filePath, string data)
{
    using var writer = new StreamWriter(filePath, true, null, 8192);
    foreach (char c in data)
        writer.WriteAsync(c);
}