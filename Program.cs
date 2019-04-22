using System;

namespace FibonacciHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new FibHeap();
            a.Insert(5);
            a.Insert(6);
            a.Insert(7);
            a.Insert(3);
            a.Insert(10);

            a.DecreaseKey(a.Min, 2);
            Console.WriteLine(a.ExtractMin().NodeData);
            Console.WriteLine(a.ExtractMin().NodeData);
            Console.WriteLine(a.ExtractMin().NodeData);
            Console.WriteLine(a.ExtractMin().NodeData);
        }
    }
}
