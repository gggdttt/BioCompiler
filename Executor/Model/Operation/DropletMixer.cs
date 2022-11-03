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

        public DropletMixer(string name, int xMix, int yMix, int xDistance, int yDistance, int repeatTimes, int line)
        {

            this.name = name;
            this.xMix = xMix;
            this.yMix = yMix;
            this.xDistance = xDistance;
            this.yDistance = yDistance;
            this.repeatTimes = repeatTimes;
            this.line = line;
        }

        public int getLine()
        {
            return line;
        }
        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
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
    }
}
