// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using Newtonsoft.Json;
using System.Collections.Immutable;

namespace Executor.Model.Operation
{
    /// <summary>
    /// repeat n times{ operations; }
    ///        int
    /// </summary>
    public class RepeatOperation : CompilerOperation
    {

        public int line { get; }
        public int repeatTimes { get; set; }

        public List<CompilerOperation> repeatOperations { get; set; }

        public RepeatOperation(int line, int repeatTimes, List<CompilerOperation> repeatOperations)
        {
            this.line = line;
            this.repeatTimes = repeatTimes;
            this.repeatOperations = repeatOperations;
        }

        public RepeatOperation(int line, int repeatTimes)
        {
            this.line = line;
            this.repeatTimes = repeatTimes;
            this.repeatOperations = new List<CompilerOperation>();
        }

        public int GetLine()
        {
            return line;
        }

        /// <summary>
        /// Traverse all the operations to check if it satisfy declaration rule
        /// </summary>
        /// <param name="declaredSet">Declared Variables</param>
        /// <param name="occupiedSet">Variables are occupied</param>
        /// <returns> return true if all repeat opeations statisfy checking rule</returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            foreach(CompilerOperation operation in repeatOperations)
            {
                if(!operation.DeclarationCheck(declaredSet, occupiedSet))
                    return false;
            }
            return true;
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // the droplet has not been initialized
            //return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0;
            return false;
        }


        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        { 
            // nothing happen
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        { 
            // don't need to execute 
            return true;
        }

        public override string ToString()
        {
            return "RepeatOperations: " + repeatTimes;
        }

    }
}
