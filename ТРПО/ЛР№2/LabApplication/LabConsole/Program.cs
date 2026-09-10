Console.WriteLine(Pow(5, 2));
Console.WriteLine(Pow(-2, 3));
Console.WriteLine(Pow(1.5, 0));
Console.WriteLine(Pow(5, -2));

Console.WriteLine(IsValidPassword("1n25+ldoW"));
Console.WriteLine(IsValidPassword("1n25+lDoБ"));
Console.WriteLine(IsValidPassword("1n25+l2o"));
Console.WriteLine(IsValidPassword("1+hW"));
Console.WriteLine(IsValidPassword("1+hW1+hW1+hW1+hW1+hW1+hW1+hW1+hW1+hW1+hW1+hW1+hW"));
Console.WriteLine(IsValidPassword("wdioionwWad"));
Console.WriteLine(IsValidPassword("djiojwDwi2123"));

static double Pow(double a, int n) => Math.Round(Math.Pow(a, n), 3);

static bool IsValidPassword(string password)
{
   return password.Length >= 8 && password.Length <= 30 &&
        password.Any(c => c >= 'a' && c <= 'z') && password.Any(c => c >= 'A' && c <= 'Z') &&
        password.Any(char.IsDigit) && password.Any(c => !char.IsLetterOrDigit(c));
}
