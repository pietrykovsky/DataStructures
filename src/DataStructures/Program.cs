using System.Diagnostics;
using Newtonsoft.Json;
using DataStructures;

class Program
{
    static void Main()
    {
        var dataLengths = new[] { 10, 100, 1000, 10000, 20000, 50000, 100000 };
        var results = new List<PerformanceResult>();

        foreach (int len in dataLengths)
        {
            results.Add(PerformanceTest<SimpleHashMap<string, int>>(len, new SimpleHashMap<string, int>(len)));
            results.Add(PerformanceTest<LengthHashMap<string, int>>(len, new LengthHashMap<string, int>(len)));
            results.Add(PerformanceTest<CharValueHashMap<string, int>>(len, new CharValueHashMap<string, int>(len)));
            results.Add(PerformanceTest<AssociativeTable<string, int>>(len, new AssociativeTable<string, int>(len)));
        }

        var jsonString = JsonConvert.SerializeObject(results);
        File.WriteAllText(@"C:\Users\mivva\Desktop\Projekty\c#\DataStructures\results\performanceData.json", jsonString);
    }

    static PerformanceResult PerformanceTest<T>(int dataLength, BaseMap<string, int> dataStructure) where T : BaseMap<string, int>
    {
        var result = new PerformanceResult
        {
            DataType = typeof(T).Name,
            DataSize = dataLength
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
        result.PutTime = sw.ElapsedMilliseconds;
        sw.Reset();

        // Get method
        sw.Start();
        for (int i = 0; i < dataLength; i++)
        {
            var value = dataStructure.Get(keys[i]);
        }
        sw.Stop();
        result.GetTime = sw.ElapsedMilliseconds;
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
        result.RemoveTime = sw.ElapsedMilliseconds;

        return result;
    }
}

public class PerformanceResult
{
    public string DataType { get; set; }
    public int DataSize { get; set; }
    public long PutTime { get; set; }
    public long GetTime { get; set; }
    public long ContainsKeyTime { get; set; }
    public long RemoveTime { get; set; }
}
