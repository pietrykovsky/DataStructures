using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using DataStructures;

class Program
{
    static void Main()
    {
        var dataLengths = new[] { 10, 100, 1000, 10000, 25000 };
        var results = new List<PerformanceResult>();

        foreach (int len in dataLengths)
        {
            results.Add(PerformanceTest<SimpleHashMap<string, int>>(len, new SimpleHashMap<string, int>(len)));
            results.Add(PerformanceTest<AssociativeTable<string, int>>(len, new AssociativeTable<string, int>(len)));
        }

        var jsonString = JsonConvert.SerializeObject(results);
        File.WriteAllText(@"C:\Users\mivva\Desktop\Projekty\c#\DataStructures\performanceData.json", jsonString);
    }

    static PerformanceResult PerformanceTest<T>(int dataLength, BaseMap<string, int> dataStructure) where T : BaseMap<string, int>
    {
        var result = new PerformanceResult
        {
            DataType = typeof(T).Name,
            DataLength = dataLength
        };

        Stopwatch sw = new Stopwatch();
        var keys = new List<string>();

        // Insertion
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            string key = Guid.NewGuid().ToString();
            keys.Add(key);
            dataStructure.Put(key, i);
        }
        sw.Stop();
        result.InsertionTime = sw.ElapsedMilliseconds;
        sw.Reset();

        // Get method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            var value = dataStructure.Get(keys[i]);
        }
        sw.Stop();
        result.RetrievalTime = sw.ElapsedMilliseconds;
        sw.Reset();

        // ContainsKey method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            bool exists = dataStructure.ContainsKey(keys[i]);
        }
        sw.Stop();
        result.ContainsKeyTime = sw.ElapsedMilliseconds;
        sw.Reset();

        // Remove method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            dataStructure.Remove(keys[i]);
        }
        sw.Stop();
        result.RemovalTime = sw.ElapsedMilliseconds;

        return result;
    }
}

public class PerformanceResult
{
    public string DataType { get; set; }
    public int DataLength { get; set; }
    public long InsertionTime { get; set; }
    public long RetrievalTime { get; set; }
    public long ContainsKeyTime { get; set; }
    public long RemovalTime { get; set; }
}
