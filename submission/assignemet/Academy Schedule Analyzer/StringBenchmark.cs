using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academy_Schedule_Analyzer
{
    [MemoryDiagnoser]
    public class StringBenchmark
    {

        [Params(100, 1000, 10000, 100000)]
        public int Iterations;
        private const string Text = "Hello";
        [Benchmark]
        public string StringConcatenation()
        {

            string result = "";

            for (int i = 0; i < Iterations; i++)
            {
                result += Text;
            }

            return result;
        }

        [Benchmark]
        public string StringBuilderConcatenation()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Iterations; i++)
            {
                result.Append(Text);
            }

            return result.ToString();
        }
    }
}
