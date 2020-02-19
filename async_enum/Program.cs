using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace async_enum
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("1- Numbers: {0}", string.Join(",", GetFiveNumbersWithPerformance()));
            Console.WriteLine("2- Numbers: {0}", string.Join(",", GetFiveNumbersWithoutPerformance()));

            Console.WriteLine("3- File: {0}", string.Join(",", GetContentFromFilesWithPerformance()));

            var filesWithAsync = GetContentFromFilesWithPerformanceAsync();

            await foreach (var file in filesWithAsync)
            {
                Console.WriteLine("4- File: {0}", file);
            }
        }

        static IEnumerable<int> GetFiveNumbersWithPerformance()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return i;
            }
        }

        static IEnumerable<int> GetFiveNumbersWithoutPerformance()
        {
            var ret = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                ret.Add(i);
            }

            return ret;
        }


        static IEnumerable<string> GetContentFromFilesWithPerformance()
        {
            var files = Directory.EnumerateFiles("C:\\temp", "*.log", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if (File.ReadAllText(file).IndexOf("test") != -1)
                {
                    yield return file;
                }
            }
        }

        static async IAsyncEnumerable<string> GetContentFromFilesWithPerformanceAsync()
        {
            var files = Directory.EnumerateFiles("C:\\temp", "*.log", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var content = await File.ReadAllTextAsync(file);

                if (content.IndexOf("test") != -1)
                {
                    yield return file;
                }
            }
        }
    }
}
