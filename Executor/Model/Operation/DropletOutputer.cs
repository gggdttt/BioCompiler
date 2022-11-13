// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Collections.Immutable;
using System.Xml.Linq;

namespace Executor.Model.Operation
{
    /// <summary>
    /// output(<droplet_name>, x, y)
    ///         string, int ,int 
    /// </summary>
    public class DropletOutputer : CompilerOperation
    {

        public int line { get; }
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }

        public DropletOutputer(string name, int xValue, int yValue, int line)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        /// <summary>
        /// If its name is not in declaredSet, return false 
        /// If it has been included in, return true and move it from declared Set to occupiedSet
        /// </summary>
        /// <param name="declaredSet"></param>
        /// <param name="occupiedSet"></param>
        /// <returns></returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {

            if (occupiedSet.Contains(name))
            {
                occupiedSet.Remove(name);
                declaredSet.Add(name);
                return true;
            }
            else return false;
        }

        public bool IsExecutable(ImmutableList<Droplet> activeDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1;
        }

        public void Active2Busy(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            // remove it in opreations
            // equals has been overriden
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            activeDroplets.Remove(d1);
        }

        public void ExecuteOperation(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            // nothing to change
        }

        public bool HasExecuted(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).ToImmutableList().Count == 0
                && busyDroplets.Where(droplet => droplet.name.Equals(name)).ToImmutableList().Count == 0;
        }
    }
}
