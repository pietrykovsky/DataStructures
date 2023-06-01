using System.Diagnostics;

namespace DataStructures
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testData = GenerateTestData(10_000);
            var sampleSizes = new int[] { 1_00, 1_000, 5_000, 10_000 };
            var iterations = 20;
            TestDataStructures(testData, iterations, sampleSizes);
        }

        private static void TestDataStructures(Node<string, int>[] data, int iterations, int[] sampleSizes)
        {
            foreach (var sampleSize in sampleSizes)
            {
                var dataStructures = new Dictionary<string, BaseMap<string, int>>
                {
                    {"Associative Table", new AssociativeTable<string, int>(sampleSize)},
                    {"Simple Hash Map", new SimpleHashMap<string, int>(sampleSize)},
                };

                foreach (var dataStructure in dataStructures)
                {
                    PopulateMap(dataStructure.Value, data, sampleSize);
                    Console.WriteLine(dataStructure.Value.Size());
                }
            }
        }

        private static void PrintMap(BaseMap<string, int> map)
        {
            foreach (var node in map)
            {
                Console.WriteLine($"{node.Key} {node.Value}");
            }
        }

        private static void PopulateMap(BaseMap<string, int> map, Node<string, int>[] data, int n)
        {
            var count = 0;
            foreach (var record in data)
            {
                if (count == n)
                    return;
                map.Put(record.Key, record.Value);
                count++;
            }
        }

        private static string GetKeyword(int n)
        {
            string baseChars = "abcdefghijklmnopqrstuvwxyz";
            if (n < baseChars.Length)
                return baseChars[n].ToString();
            return GetKeyword(n / baseChars.Length - 1) + baseChars[n % baseChars.Length];
        }

        private static Node<string, int>[] GenerateTestData(int n)
        {
            var data = new Node<string, int>[n];
            for (var i = 0; i < n; i++)
            {
                data[i] = new Node<string, int>(key: GetKeyword(i), value: i + 1);
            }
            return data;
        }

        private static long measureTime(Action<string> removeFunc, string key)
        {
            var watch = Stopwatch.StartNew();
            removeFunc(key);
            watch.Stop();
            var time = watch.ElapsedMilliseconds;
            return time;
        }
    }
}