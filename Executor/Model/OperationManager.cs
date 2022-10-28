// Copyright (c) 2022 Wenjie Fan
// Department of Applied Mathematics
// Technical University of Denmark
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
