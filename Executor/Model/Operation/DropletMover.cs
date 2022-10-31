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
    public class DropletMover: CompilerOperation
    {

        public string name { get; }
        public int xDest { get; }
        public int yDest { get; }

        public DropletMover(string name, int xDest, int yDest )
        {
            this.name = name;
            this.xDest = xDest;
            this.yDest = yDest;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
