// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

namespace Executor.Model.Operation
{
    /// <summary>
    /// merge(<out_dest_droplet_name>,<in_1_droplet_name>,<in_2_droplet_name>,x_dest,y_dest
    ///         string, string, string, int, int 
    /// </summary>
    public class DropletMerger: CompilerOperation
    {
        
        public string outDropletName { get; }
        public string inDroplet1Name { get; }
        public string inDroplet2Name { get; }
        public int xDest { get; }
        public int yDest { get; }

        public DropletMerger(string outDropletName, string inDroplet1Name, string inDroplet2Name, int xValue, int yValue)
        {
            this.outDropletName = outDropletName;
            this.inDroplet1Name = inDroplet1Name;
            this.inDroplet2Name = inDroplet2Name;
            xDest = xValue;
            yDest = yValue;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
