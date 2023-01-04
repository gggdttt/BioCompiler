// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using ToolSupporter.BioExceptions;

namespace Executor.Model.Operation
{
    /// <summary>
    /// input(       <droplet_name>, x,   y,   volume);
    /// member type:      string,   int, int, float
    /// </summary>
    public class DropletInputer : CompilerOperation
    {
        public int line { get; }
        public string name { get; set; }
        public int xValue { get; }
        public int yValue { get; }
        public double size { get; }

        static List<Droplet> RecordDroplets = new List<Droplet>();

        public DropletInputer(string dropletName, int xValue, int yValue, double size, int line)
        {
            this.name = dropletName;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        /// <summary>
        /// If the name is in declared Set and not in occupied set, execute input
        /// If its name is not in declaredSet and not in occupied set, the droplet has not been declared 
        /// If its name is in occupied set, the droplet has not been released
        /// </summary>
        /// <param name="declaredSet"></param>
        /// <param name="occupiedSet"></param>
        public void DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {

            if (declaredSet.Contains(name))
            {
                declaredSet.Remove(name);
                occupiedSet.Add(name);
            }
            else if (occupiedSet.Contains(name))
            {
                throw new VariableNotReleasedException(line);
            }
            else if (!declaredSet.Contains(name) && !occupiedSet.Contains(name))
            {
                throw new DropletNotDeclaredException(line);
            }
            else throw new Exception();

        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // the droplet has not been initialized
            if (activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0
                && busyDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0
                && RecordDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0)
            {
                RecordDroplets.Add(new Droplet(name, xValue, yValue, size));
                return true;
            }
            return false;
        }


        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        {
            Droplet d = RecordDroplets.Where(droplet => droplet.name.Equals(name)).First();
            RecordDroplets.Remove(d);
            //generate a new droplet
            activeDroplets.Add(d);
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            Droplet d = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1
                && d.xValue == xValue
                && d.yValue == yValue
                && d.volume == size;
        }

        public override string ToString()
        {
            return "DropletInputer: " + name;
        }
    }
}
