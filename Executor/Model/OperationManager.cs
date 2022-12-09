﻿// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using Executor.Model.Operation;

namespace Executor.Model
{
    public class OperationManager
    {

        int x;

        int y;

        // Operations from json
        List<CompilerOperation> operations;

        List<CompilerOperation> executingOperations;

        public List<Droplet> activeDroplets { get; set; }

        public List<Droplet> busyDroplets { get; set; }


        public OperationManager(List<CompilerOperation> operations, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.operations = this.GetOperationsWithoutRepeat(operations);
            executingOperations = new List<CompilerOperation>();
            activeDroplets = new List<Droplet>();
            busyDroplets = new List<Droplet>();
        }

        /// <summary>
        /// filter operations
        /// </summary>
        /// <param name="operations"></param>
        /// <returns></returns>
        private List<CompilerOperation> GetOperationsWithoutRepeat(List<CompilerOperation> operations)
        {
            List<CompilerOperation> compilerOperations= new List<CompilerOperation>();
            foreach (CompilerOperation operation in operations)
            {
                if(operation is RepeatOperation)
                {
                    compilerOperations.AddRange(GetOperationsWithoutRepeat(((RepeatOperation)operation).repeatOperations));
                }
                else 
                    compilerOperations.Add(operation);
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
/*            Console.WriteLine("\n\n\n before executing");
            DebugPrint();*/
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

/*          Console.WriteLine("\n\n\n executing");
            DebugPrint();*/
        }

        /// <summary>
        /// 1. remove opreation in executingOpreations
        /// </summary>
        public void AfterExecute()
        {
            List<CompilerOperation> executedOperations = executingOperations.Where(t => t.HasExecuted(activeDroplets, busyDroplets)).ToList();
            // remove executed 
            executingOperations = executingOperations.Except(executedOperations).ToList();

            Console.WriteLine("\n\n\n after executing");
            DebugPrint();
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
