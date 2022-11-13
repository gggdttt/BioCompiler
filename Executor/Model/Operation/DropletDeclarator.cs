// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Collections.Immutable;

namespace Executor.Model.Operation
{
    /// <summary>
    /// droplet <name>;
    ///         string
    /// </summary>
    public class DropletDeclarator : CompilerOperation
    {

        public string name { get; }
        public int line { get; }

        public DropletDeclarator(string name, int line)
        {
            this.name = name;
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        /// <summary>
        /// If its name is not in declaredSet, add and return true
        /// If it has been included in any set, return false
        /// </summary>
        /// <param name="declaredSet">Declared Variables</param>
        /// <param name="occupiedSet">Variables are occupied</param>
        /// <returns> return true if it's added successfully, otherwise return false</returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            if (!declaredSet.Contains(name) && !occupiedSet.Contains(name))
            {
                declaredSet.Add(name);
                return true;
            }
            else return false;
        }

        public bool IsExecutable(ImmutableList<Droplet> activeDroplets)
        {
            // the droplet has not been initialized
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0;
        }


        public void Active2Busy(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            // nothing to consume 
            // nothing to generate
        }

        public void ExecuteOperation(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets) 
        { 
            // nothing happen
        }

        public bool HasExecuted(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        { 
            // don't need to execute 
            return true;
        }


    }
}
