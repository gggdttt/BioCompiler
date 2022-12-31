// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using ToolSupporter.BioExceptions;

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
        /// If its name has been in dec set, it has been declared
        /// If its name has been in occ set, it has been initialized
        /// It its name not in dec or occ, add it to dec
        /// </summary>
        /// <param name="declaredSet">Declared Variables</param>
        /// <param name="occupiedSet">Variables are occupied</param>
        public void DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            if (declaredSet.Contains(name))
            {
                throw new DropletDeclaredMoreThanOnceException(line);
            }
            else if (occupiedSet.Contains(name))
            {
                throw new DropletDeclaredMoreThanOnceException(line);
            }
            else
            {
                declaredSet.Add(name);
            }

        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // the droplet has not been initialized
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0;
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
            return "DropletDeclarator: " + name;
        }

    }
}
