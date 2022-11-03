// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Model.Operation
{
    /// <summary>
    /// store(<droplet_name>,x,y, time)
    ///     string, int, int , float
    /// </summary>
    public class DropletStorer : CompilerOperation
    {
        public int line { get; }
        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public double latency { get; }

        public DropletStorer(string name, int xValue, int yValue, double latency, int line)
        {

            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.latency = latency;
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
