using System;
using System.Collections.Generic;
using System.Text;

namespace SubsidyCalculation
{
    class Handler
    {
        public static void Notify(object sender, string text) => Console.WriteLine(text);
        public static void Exception(object sender, Tuple<string, Exception> text)
        {
            Console.WriteLine(text.Item1);
            throw text.Item2;
        }
    }
}
