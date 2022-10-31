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
    public class DropletStorer: CompilerOperation
    {

        public string name { get; }
        public int xValue { get; }
        public int yValue { get; }
        public double latency { get; }

        public DropletStorer(string name,  int xValue, int yValue, double latency)
        {

            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.latency = latency;
        }


        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
