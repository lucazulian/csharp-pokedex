using System;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace CsharpPokedex.Domain.UnitTests.Helpers
{
    public static class Comparer
    {
        private static readonly CompareLogic ObjectComparer = new(new ComparisonConfig
        {
            CompareChildren = true,
            CompareProperties = true,
            CompareReadOnly = true,
            IgnoreCollectionOrder = true
        });

        public static bool Compare<T>(T expected, T actual)
        {
            var result = ObjectComparer.Compare(expected, actual);
            if (result.Differences.Any())
            {
                Console.WriteLine($"Differences between expected and actual:\r\n {result.DifferencesString}");
            }

            return result.AreEqual;
        }
    }
}