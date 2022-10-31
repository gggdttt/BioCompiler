// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolSupporter.FileOperator
{
    internal class BioFileReader
    {
        private static ImmutableArray<string> Read(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath).ToImmutableArray();
        }

    }
}
