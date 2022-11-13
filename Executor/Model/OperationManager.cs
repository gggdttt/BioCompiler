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

        int x;

        int y;

        // Operations from json
        ImmutableList<CompilerOperation> operations;

        ImmutableList<CompilerOperation> executingOperations;

        ImmutableList<Droplet> activeDroplets;

        ImmutableList<Droplet> busyDroplets;


        public OperationManager(ImmutableList<CompilerOperation> operations, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.operations = operations;
            executingOperations = ImmutableList.Create<CompilerOperation>();
            activeDroplets = ImmutableList.Create<Droplet>();
            busyDroplets = ImmutableList.Create<Droplet>();
        }

        /// <summary>
        /// find all the executable operations, add them to executingOperations, move active droplets to busy droplets
        /// </summary>
        public void BeforeExecuting()
        {
            ImmutableList<CompilerOperation> executalbeOperations = operations.Where(t => t.IsExecutable(activeDroplets)).ToImmutableList();
            foreach (CompilerOperation op in executalbeOperations)
            {
                op.Active2Busy(activeDroplets, busyDroplets);
            }
            // remove all executable operations and add them to executingOperations
            operations = operations.RemoveAll(t => t.IsExecutable(activeDroplets));
            executingOperations = executingOperations.AddRange(executalbeOperations);
        }

        /// <summary>
        /// Keypart, need to create a clock to execute step by step
        /// </summary>
        public void Executing()
        {
            // intergration with a clock
            foreach (CompilerOperation op in executingOperations)
            {
                op.ExecuteOperation(activeDroplets, busyDroplets);
            }
        }

        /// <summary>
        /// 1. remove opreation in executingOpreations
        /// </summary>
        public void AfterExecute()
        {
            ImmutableList<CompilerOperation> executedOperations = operations.Where(t => t.HasExecuted(activeDroplets, busyDroplets)).ToImmutableList();
            executingOperations = executingOperations.RemoveAll(t => t.HasExecuted(activeDroplets, busyDroplets));
        }

    }
}
