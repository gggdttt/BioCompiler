// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Xml.Linq;

namespace Executor.Model.Operation
{
    /// <summary>
    /// merge(<out_dest_droplet_name>,<in_1_droplet_name>,<in_2_droplet_name>,x_dest,y_dest
    ///         string, string, string, int, int 
    /// </summary>
    public class DropletMerger : CompilerOperation
    {

        public int line { get; }
        public string outDropletName { get; }
        public string inDroplet1Name { get; }
        public string inDroplet2Name { get; }
        public int xDest { get; }
        public int yDest { get; }

        public DropletMerger(string outDropletName, string inDroplet1Name, string inDroplet2Name, int xValue, int yValue, int line)
        {
            this.outDropletName = outDropletName;
            this.inDroplet1Name = inDroplet1Name;
            this.inDroplet2Name = inDroplet2Name;
            xDest = xValue;
            yDest = yValue;
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

            if (declaredSet.Contains(outDropletName)
                && occupiedSet.Contains(inDroplet1Name)
                && occupiedSet.Contains(inDroplet2Name))
            {
                declaredSet.Remove(outDropletName);
                occupiedSet.Add(outDropletName);

                occupiedSet.Remove(inDroplet1Name);
                declaredSet.Add(inDroplet1Name);

                occupiedSet.Remove(inDroplet2Name);
                declaredSet.Add(inDroplet2Name);
                return true;
            }
            else return false;
        }
    }
}
