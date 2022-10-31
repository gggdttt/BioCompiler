// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System.Collections.Immutable;
using Executor.Model.Operation;

namespace Executor.Model
{
    internal class OperationManager
    {
        private ImmutableArray<Droplet> droplets;
        private ChipArea chipArea;
        private ImmutableArray<CompilerOperation> operations;
    }
}
