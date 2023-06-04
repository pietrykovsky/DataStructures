using System.Diagnostics;

namespace DataStructures
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sampleSizes = new int[] { 100, 1000, 5000, 10000, 20000 };
            var testData = GenerateTestData(sampleSizes.Last());
            var iterations = 20;
            MeasureDataStructures(testData, iterations, sampleSizes);
        }

        private static void MeasureDataStructures(Node<string, int>[] data, int iterations, int[] sampleSizes)
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
                    MeasureDataStructure(dataStructure, iterations, sampleSize);
                }
            }
        }

        private static void MeasureDataStructure(KeyValuePair<string, BaseMap<string, int>> dataStructure, int iterations, int sampleSize)
        {
            var averageGet = 0l;
            var averagePut = 0l;
            var averageRemove = 0l;
            var averageContainsKey = 0l;

            for (var i = 0; i < iterations; i++)
            {
                averageRemove += MeasureMethodTime(dataStructure.Value.Remove, "bc");
                averageContainsKey += MeasureMethodTime(dataStructure.Value.ContainsKey, "bc");
                averagePut += MeasureMethodTime(dataStructure.Value.Put, "bc", 5);
                averageGet += MeasureMethodTime(dataStructure.Value.Get, "bc");
            }

            averageRemove /= iterations;
            averageContainsKey /= iterations;
            averagePut /= iterations;
            averageGet /= iterations;

            Console.WriteLine(dataStructure.Key);
            Console.WriteLine($"sample size: {sampleSize}");
            Console.WriteLine($"Get average: {averageGet} ms");
            Console.WriteLine($"Put average: {averagePut} ms");
            Console.WriteLine($"Remove average: {averageRemove} ms");
            Console.WriteLine($"ContainsKey average: {averageContainsKey} ms\n");
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

        private static long MeasureMethodTime<TKey>(Action<TKey> removeFunc, TKey key)
        {
            var watch = Stopwatch.StartNew();
            removeFunc(key);
            watch.Stop();
            var time = watch.ElapsedMilliseconds;
            return time;
        }

        private static long MeasureMethodTime<TKey, TResult>(Func<TKey, TResult> removeFunc, TKey key)
        {
            var watch = Stopwatch.StartNew();
            removeFunc(key);
            watch.Stop();
            var time = watch.ElapsedMilliseconds;
            return time;
        }

        private static long MeasureMethodTime<TKey, TValue>(Action<TKey, TValue> removeFunc, TKey key, TValue value)
        {
            var watch = Stopwatch.StartNew();
            removeFunc(key, value);
            watch.Stop();
            var time = watch.ElapsedMilliseconds;
            return time;
        }
    }
}