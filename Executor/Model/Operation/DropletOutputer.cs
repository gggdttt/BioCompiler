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
    public class DropletOutputer : CompilerOperation
    {

        public int line { get; }
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }

        public DropletOutputer(string name, int xValue, int yValue, int line)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
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

            if (occupiedSet.Contains(name))
            {
                occupiedSet.Remove(name);
                declaredSet.Add(name);
                return true;
            }
            else return false;
        }
    }
}
