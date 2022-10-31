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
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public float size { get; }
        public int _order_id { get; }

        public Droplet execResult { get; set; }

        public DropletInputer(string dropletName, int xValue, int yValue, float size)
        {
            // TODO : add order_id here 
            name = dropletName;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
        }

        public void Executed()
        {
            execResult = new Droplet(name, xValue, yValue, size, false);
        }
    }
}
