// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using CommandLine;

namespace BioCompiler

{
    internal class Option
    {
        [Option('s', "source", Required = true, HelpText = ".input file")]
        public string? Source { get; set; }

        [Option('o', "output", Required = true, HelpText = ".output path")]
        public string? Output { get; set; }

    }

}
