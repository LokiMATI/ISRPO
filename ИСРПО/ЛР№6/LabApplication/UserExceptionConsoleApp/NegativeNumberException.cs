namespace UserExceptionConsoleApp;

public class NegativeNumberException : Exception
{
    public NegativeNumberException(string message) : base(message) { }
}
