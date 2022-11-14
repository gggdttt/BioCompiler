// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using CommandLine;

namespace Executor
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<ExecutorLaunchOption>(args)
               .WithParsed(opts =>
               {
                   Environment.Exit(new ProcessExecutor(opts).Execute());
               })
               .WithNotParsed(errs =>
               {
                   Environment.Exit(1);
               });
        }
    }
}
