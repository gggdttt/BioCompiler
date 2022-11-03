// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using CommandLine;

namespace BioCompiler

{
    internal class Option
    {
        [Option('s', "source", Required = true, HelpText = ".app file to build documentation for")]
        public string? Source { get; set; }

        [Option('o', "output", Required = true, HelpText = ".app file to build documentation for")]
        public string? Output { get; set; }

    }

}
