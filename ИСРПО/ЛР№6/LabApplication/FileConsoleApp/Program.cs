try
{
    using var file = new StreamReader(@"C:\temp\ispp31\ИСРПО\ЛР№6\Tasks3.txt");
    var text = file.ReadToEnd();

    int count = 0;
    foreach (var ch in text)
    {
        if (char.IsDigit(ch))
        {
            var number = Convert.ToInt32(ch);
            if (number % 2 == 0)
                count++;
        }
    }
    Console.WriteLine(count);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("Файл не был обнаружен!");
}
catch (Exception)
{

}