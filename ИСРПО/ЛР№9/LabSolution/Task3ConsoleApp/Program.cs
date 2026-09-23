Dictionary<int, int> _fibonacciCache = new();

for (int i = 0; i < 20; i++)
    Console.WriteLine($"Fib({i}) = {Fibonacci(i)}");

int Fibonacci(int n)
{
    if (_fibonacciCache.TryGetValue(n, out int value))
        return value;

    if (n <= 1)
        return n;

    var result = Fibonacci(n - 1) + Fibonacci(n - 2);
    _fibonacciCache[n] = result;
    return result;
}