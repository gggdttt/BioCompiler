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
