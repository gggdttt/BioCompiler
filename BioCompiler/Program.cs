// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
namespace BioCompiler

{
    using System;
    using Antlr4.Runtime;
    using System.Text;
    using CommandLine;
    using static SyntaxParser;
    using System.IO;
    using BioCompiler.Compiler;
    using Executor.Model.Operation;
    using System.Text.Json;
    using Newtonsoft.Json;
    using BioCompiler.Checker;
    using System.Collections.Immutable;

    internal static class Program
    {
        private static void Main(string[] args)
        {

            CommandLine.Parser.Default.ParseArguments<Option>(args)
               .WithParsed<Option>(opts =>
               {
                   Environment.Exit(new Runner(opts).run());
               })
               .WithNotParsed(errs =>
               {
                   Environment.Exit(1);
               });
        }
    }
}
