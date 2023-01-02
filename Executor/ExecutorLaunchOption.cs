// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using CommandLine;

namespace Executor

{ 
    public class ExecutorLaunchOption
    {
        [Option('s', "source", Required = true, HelpText = ".input file")]
        public string? Source { get; set; }

        [Option('c', "config", Required = true, HelpText = ".input file")]
        public string? Config { get; set; }

    }

}
