// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Model.Operation;

namespace Executor.Model
{
    public class OperationManager
    {
        // Operations from json
        List<CompilerOperation> operations;

        // operations are executing
        List<CompilerOperation> executingOperations;

        public List<Droplet> activeDroplets { get; set; }

        public List<Droplet> busyDroplets { get; set; }

        
        public OperationManager(List<CompilerOperation> operations)
        {
            this.operations = GetOperationsWithoutRepeat(operations);
            executingOperations = new List<CompilerOperation>();
            activeDroplets = new List<Droplet>();
            busyDroplets = new List<Droplet>();
        }

        /// <summary>
        /// 1. If one operation is a normal operation, add it to compilerOperation
        /// 2. If one operation is a repeat opeartion, add the opeartions inside this repeat opeartion to the compilerOperation 
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        private List<CompilerOperation> GetOperationsWithoutRepeat(List<CompilerOperation> operations)
        {
            List<CompilerOperation> compilerOperations = new List<CompilerOperation>();
            foreach (CompilerOperation operation in operations)
            {
                if (operation is RepeatOperation)
                {
                    // add the opreations to the final list of operations recursively
                    compilerOperations.AddRange(GetOperationsWithoutRepeat(((RepeatOperation)operation).repeatOperations));
                }
                else
                {
                    compilerOperations.Add(operation);
                }
            }
            return compilerOperations;
        }

        /// <summary>
        /// find all the executable operations, add them to executingOperations, move active droplets to busy droplets
        /// </summary>
        public void BeforeExecuting()
        {
            List<CompilerOperation> executalbeOperations = operations.Where(t => t.IsExecutable(activeDroplets, busyDroplets)).ToList();

            // remove all executable operations and add them to executingOperations
            operations = operations.Except(executalbeOperations).ToList();
            executingOperations.AddRange(executalbeOperations);

        }

        /// <summary>
        /// Keypart, need to create a clock to execute step by step
        /// </summary>
        public void Executing(MovementManager manager)
        {
            // intergration with a clock
            foreach (CompilerOperation op in executingOperations)
            {
                op.ExecuteOperation(activeDroplets, busyDroplets, manager);
            }
        }

        /// <summary>
        /// 1. remove opreation in executingOpreations
        /// </summary>
        public void AfterExecute()
        {
            List<CompilerOperation> executedOperations = executingOperations.Where(t => t.HasExecuted(activeDroplets, busyDroplets)).ToList();
            // remove executed 
            executingOperations = executingOperations.Except(executedOperations).ToList();
        }

        public bool AllTasksCompleted()
        {
            return operations.Count() == 0 && executingOperations.Count() == 0;
        }

        private void DebugPrint()
        {
            Console.WriteLine("======================================================:");

            Console.WriteLine("-----------------operations:");
            foreach (CompilerOperation op in operations)
            {
                Console.WriteLine(op);
            }

            Console.WriteLine("-----------------executingOperations:");
            foreach (CompilerOperation op in executingOperations)
            {
                Console.WriteLine(op);
            }

            Console.WriteLine("-----------------activeDroplets:");
            foreach (Droplet d in activeDroplets)
            {
                Console.WriteLine(d);
            }

            Console.WriteLine("-----------------busyDroplets:");
            foreach (Droplet d in busyDroplets)
            {
                Console.WriteLine(d);
            }
        }
    }
}
