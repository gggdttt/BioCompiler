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

        /*        [Option('r', "router", Default = "astar", HelpText = ".output path")]
                public string Router { get; set; }*/

        /*        [Option('c', "column", Default = 36, HelpText = ".output path")]
                public int Column { get; set; }

                [Option('w', "row", Default = 20, HelpText = ".output path")]
                public int Row { get; set; }*/

    }

}
