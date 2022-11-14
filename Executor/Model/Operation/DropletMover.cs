// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


namespace Executor.Model.Operation
{
    /// <summary>
    /// move(<droplet_name>, x_dest, y_dest);
    ///         string, int ,int 
    /// </summary>
    public class DropletMover : CompilerOperation
    {

        public int line { get; }
        public string name { get; }
        public int xDest { get; }
        public int yDest { get; }

        public DropletMover(string name, int xDest, int yDest, int line)
        {
            this.name = name;
            this.xDest = xDest;
            this.yDest = yDest;
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
            return occupiedSet.Contains(name);
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            if(activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            return false;
        }

        private void Active2Busy(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            //droplet active->busy
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);
        }

        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            Droplet d1 = busyDroplets.Where(droplet => droplet.name.Equals(name)).First();
            busyDroplets.Remove(d1);
            // TODO remove, assume now it's finished at once
            d1.xValue = xDest;
            d1.yValue = yDest;
            //============
            activeDroplets.Add(d1);

            int waitTime = Math.Abs(d1.xValue - xDest) + Math.Abs(d1.yValue - yDest);
            //TODO
            Console.WriteLine("Is waiting for Droplet Moving, need time:" + waitTime);
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            List<Droplet> testDroplets = activeDroplets.Where(droplet => droplet.name.Equals(name)).ToList();
            return testDroplets.Count() == 1
                && testDroplets.First().xValue == xDest
                && testDroplets.First().yValue == yDest;
        }

        public override string ToString()
        {
            return "DropletMover: " + name;
        }
    }
}
