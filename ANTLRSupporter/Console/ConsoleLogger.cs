// Copyright (c) 2022 Wenjie Fan
// Department of Applied Mathematics
// Technical University of Denmark
namespace ToolSupporter.Console
{
    using System;
    class ConsoleLogger
    {
        public void Error(string value, params object[] arg)
        {
            Console.WriteLine(value, arg);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string value, params object[] arg)
        {
            Console.WriteLine(value, arg);
        }
    }
}
