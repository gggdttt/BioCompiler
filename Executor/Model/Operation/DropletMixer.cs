// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


namespace Executor.Model.Operation
{
    /// <summary>
    /// mix(<droplet_name>,x_mix,y_mix,size_x,size_y,repeat_times)
    ///             string, int ,int ,int ,int int
    /// </summary>
    public class DropletMixer : CompilerOperation
    {

        public int line { get; }
        public string name { get; }
        public int xMix { get; }
        public int yMix { get; }
        public int xDistance { get; }
        public int yDistance { get; }
        public int repeatTimes { get; }

        private int timesCounter;

        public DropletMixer(string name, int xMix, int yMix, int xDistance, int yDistance, int repeatTimes, int line)
        {

            this.name = name;
            this.xMix = xMix;
            this.yMix = yMix;
            this.xDistance = xDistance;
            this.yDistance = yDistance;
            this.repeatTimes = repeatTimes;
            this.line = line;
            timesCounter = 0;
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
            // droplet busy->active
            Droplet d1 = busyDroplets.Where(droplet => droplet.name.Equals(name)).First();
            busyDroplets.Remove(d1);
            activeDroplets.Add(d1);
            // TODO: timesCounter ++;
            timesCounter = repeatTimes;
            // TODO:
            Console.WriteLine("Is waiting for Droplet Mixing, need time:");
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1
                && repeatTimes == timesCounter;
        }

        public override string ToString()
        {
            return "DropletMixer: " + name;
        }
    }
}
