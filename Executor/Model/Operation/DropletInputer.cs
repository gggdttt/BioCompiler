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
        public int line { get; }
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public double size { get; }
        public int _order_id { get; }

        public Droplet execResult { get; set; }

        public DropletInputer(string dropletName, int xValue, int yValue, double size, int line)
        {
            // TODO : add order_id here 
            name = dropletName;
            this.xValue = xValue;
            this.yValue = yValue;
            this.size = size;
            this.line = line;
        }

        public int getLine()
        {
            return line;
        }
        public void Executed()
        {
            execResult = new Droplet(name, xValue, yValue, size, false);
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

            if (declaredSet.Count() == 0) return false;
            if (declaredSet.Contains(name))
            {
                declaredSet.Remove(name);
                occupiedSet.Add(name);
                return true;
            }
            else return false;
        }

    }
}
