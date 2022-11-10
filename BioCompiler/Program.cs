// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace BioCompiler

{
    using System;
    using CommandLine;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Option>(args)
               .WithParsed<Option>(opts =>
               {
                   Environment.Exit(new Runner(opts).Run());
               })
               .WithNotParsed(errs =>
               {
                   Environment.Exit(1);
               });
        }
    }
}
