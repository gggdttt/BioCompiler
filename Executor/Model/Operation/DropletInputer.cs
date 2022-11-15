// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


namespace Executor.Model.Operation
{
    /// <summary>
    /// input(       <droplet_name>, x,   y,   size);
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
        /// If its name is not in declaredSet, return false 
        /// If it has been included in, return true and move it from declared Set to occupiedSet
        /// </summary>
        /// <param name="declaredSet"></param>
        /// <param name="occupiedSet"></param>
        /// <returns></returns>
        public bool DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            if (declaredSet.Count() == 0) return false;
            if (declaredSet.Contains(name))
            {
                declaredSet.Remove(name);
                occupiedSet.Add(name);
                return true;
            }
            else return false;
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // the droplet has not been initialized
            if(activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 0
                && RecordDroplets.Where(droplet => droplet.name.Equals(name)).Count()==0)
            {
                RecordDroplets.Add(new Droplet(name, xValue, yValue, size));
                return true;
            }
            return false;
        }


        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
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
                && d.size ==size;
        }

        public override string ToString()
        {
            return "DropletInputer: " + name;
        }
    }
}
