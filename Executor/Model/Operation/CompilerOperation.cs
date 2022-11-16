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
        /// 
        /// </summary>
        /// <param name="activeDroplets">Active droplets of Chip</param>
        /// <param name="busyDroplets">Busy Droplet of Chip</param>
        /// <returns></returns>
        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeDroplets"></param>
        /// <param name="busyDroplets"></param>
        /// <param name="manager"></param>
        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager);


        /// <summary>
        /// Check if current operation has completed by comparing the current position and dest position
        /// </summary>
        /// <returns></returns>
        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets);


    }
}
