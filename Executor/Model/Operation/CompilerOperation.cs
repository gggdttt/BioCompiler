// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model.Operation
{

    public interface CompilerOperation
    {
        /// <summary>
        /// Get the line where operation is defined
        /// </summary>
        /// <returns>the line number in source code</returns>
        public int GetLine();

        /// <summary>
        /// Check if the droplet declaration is well defined
        /// </summary>
        /// <param name="declaredSet"> The droplet declared </param>
        /// <param name="occupiedSet"> The droplet is being used</param>
        /// <returns>true if there is no syntax error, false if current operation contains any syntax error </returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet);


        ///============================================================================================================
        /// The following functions are for executor


        /// <summary>
        /// Check if the operation is executable. If it is executable, move the droplets it need from activeDroplets to busyDroplets  and return True, other wise do nothing and return false
        /// </summary>
        /// <param name="activeDroplets">Active droplets of Chip</param>
        /// <param name="busyDroplets">Busy Droplet of Chip</param>
        /// <returns>Return True if it is executable, otherwise return false </returns>
        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets);

        /// <summary>
        /// Execute the operation by steps. Every unit time, execute one step of operation. For example, move droplet from (0,0) to (1,1), it needs 3 steps, so it need 3 unit time to run.
        /// </summary>
        /// <param name="activeDroplets">Active droplets of Chip</param>
        /// <param name="busyDroplets">The droplets are busy/occupied </param>
        /// <param name="manager">Use movement manager to move droplets</param>
        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager);


        /// <summary>
        /// Check if current operation has completed by comparing the current position and dest position
        /// </summary>
        /// <returns></returns>
        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets);


    }
}
