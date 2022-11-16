// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


namespace Executor.Model.Operation
{
    /// <summary>
    /// store(<droplet_name>,x,y, latency)
    ///     string, int, int , float
    /// </summary>
    public class DropletStorer : CompilerOperation
    {
        public int line { get; }
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public double latency { get; }
        public double time { get; set; }

        public DropletStorer(string name, int xValue, int yValue, double latency, int line)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.latency = latency;
            this.line = line;
            time = 0;
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
            return occupiedSet.Contains(name);
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            if (activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            return false;
        }

        private void Active2Busy(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);
        }

        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        {
            Droplet d1 = busyDroplets.Where(droplet => droplet.name.Equals(name)).First();
            busyDroplets.Remove(d1);
            activeDroplets.Add(d1);
            // TODO: remove, incre time until time == latency
            time = latency;
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).ToList().Count == 1
                && busyDroplets.Where(droplet => droplet.name.Equals(name)).ToList().Count == 0
                && time == latency;
        }

        public override string ToString()
        {
            return "DropletStorer: " + name;
        }
    }
}
