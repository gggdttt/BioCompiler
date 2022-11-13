// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Collections.Immutable;

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
        /// <param name="activeDroplets"></param>
        /// <returns></returns>
        public bool IsExecutable(ImmutableList<Droplet> activeDroplets);

        /// <summary>
        /// Check if the 
        /// </summary>
        /// <param name="activeDroplets">Active droplets of Chip</param>
        /// <param name="busyDroplets">Busy Droplet of Chip</param>
        public void Active2Busy(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeDroplets"></param>
        /// <param name="busyDroplets"></param>
        public void ExecuteOperation(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets);


        /// <summary>
        /// Check if current operation has completed by comparing the current position and dest position
        /// </summary>
        /// <returns></returns>
        public bool HasExecuted(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets);


    }
}
