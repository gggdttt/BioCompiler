// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model.Operation
{
    /// <summary>
    /// output(<droplet_name>, x, y)
    ///         string, int ,int 
    /// </summary>
    public class DropletOutputer: CompilerOperation
    {

        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }

        public DropletOutputer(string name,  int xValue, int yValue)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
