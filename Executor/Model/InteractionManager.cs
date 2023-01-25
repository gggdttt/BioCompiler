// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)



namespace Executor.Model
{
    public class InteractionManager
    {
        /// <summary>
        /// This property is used to check what's the mode of running
        /// True means this is run on Async mode
        /// False means this is run on Sync mode
        /// </summary>
        public bool IsAsync { get; set; }

        public InteractionManager(bool mode) {
        this.IsAsync = mode;
        }

    }
}
