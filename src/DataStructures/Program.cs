using DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        var dataLengths = new[] { 10, 100, 1000, 10000, 25000 };

        foreach (int len in dataLengths)
        {
            Console.WriteLine($"Testing with data length: {len}");
            PerformanceTest<SimpleHashMap<string, int>>(len, new SimpleHashMap<string, int>(len));
            PerformanceTest<AssociativeTable<string, int>>(len, new AssociativeTable<string, int>(len));
            Console.WriteLine();
        }
    }

    static void PerformanceTest<T>(int dataLength, BaseMap<string, int> dataStructure)
    {
        Stopwatch sw = new Stopwatch();
        var keys = new List<string>();

        Console.WriteLine($"Testing Performance of {typeof(T).Name}:");

        // Insertion
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            string key = Guid.NewGuid().ToString();
            keys.Add(key);
            dataStructure.Put(key, i);
        }
        sw.Stop();
        Console.WriteLine($"Insertion: {sw.ElapsedMilliseconds}ms");
        sw.Reset();

        // Get method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            var value = dataStructure.Get(keys[i]);
        }
        sw.Stop();
        Console.WriteLine($"Retrieval: {sw.ElapsedMilliseconds}ms");
        sw.Reset();

        // ContainsKey method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            bool exists = dataStructure.ContainsKey(keys[i]);
        }
        sw.Stop();
        Console.WriteLine($"ContainsKey: {sw.ElapsedMilliseconds}ms");
        sw.Reset();

        // Remove method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            dataStructure.Remove(keys[i]);
        }
        sw.Stop();
        Console.WriteLine($"Removal: {sw.ElapsedMilliseconds}ms");
    }
}
