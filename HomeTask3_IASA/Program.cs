using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        const int N = 10_000_000;

        Console.WriteLine("Початок обчислень...");
        Stopwatch sw = Stopwatch.StartNew();

        long totalSteps = 0;


        Parallel.For(1, N + 1, () => 0L, (i, loop, localSum) =>
        {
            localSum += CollatzSteps(i);
            return localSum;
        },
        localSum => System.Threading.Interlocked.Add(ref totalSteps, localSum));

        sw.Stop();

        double avgSteps = (double)totalSteps / N;

        Console.WriteLine($"Обчислення завершено за {sw.ElapsedMilliseconds / 1000.0:F2} с");
        Console.WriteLine($"Середня кількість кроків до 1 для {N:N0} чисел = {avgSteps:F2}");
    }


    static int CollatzSteps(long n)
    {
        int steps = 0;
        while (n != 1)
        {
            if ((n & 1) == 0) 
                n /= 2;
            else
                n = 3 * n + 1;
            steps++;
        }
        return steps;
    }
}
