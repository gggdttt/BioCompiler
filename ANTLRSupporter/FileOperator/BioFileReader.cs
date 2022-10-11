// Copyright (c) 2022 Wenjie Fan
// Department of Applied Mathematics
// Technical University of Denmark
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
