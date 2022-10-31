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
    public class DropletMixer: CompilerOperation
    {

        public string name { get; }
        public int xMix { get; }
        public int yMix { get; }
        public int xDistance { get;}
        public int yDistance { get;}
        public int repeatTimes { get;}

        public DropletMixer(string name,  int xMix, int yMix, int xDistance, int yDistance, int repeatTimes)
        {

            this.name = name;
            this.xMix = xMix;
            this.yMix = yMix;
            this.xDistance = xDistance;
            this.yDistance = yDistance;
            this.repeatTimes = repeatTimes;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
