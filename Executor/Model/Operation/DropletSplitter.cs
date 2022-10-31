// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Executor.Model.Operation
{
    /// <summary>
    /// split(<out_dest_name1>,<out_dest_name2>,<in_droplet_name>,left_x_dest, left_y_dest, right_x_dest, right_y_dest, ratio);
    /// string, string, strig, int, int ,int, int, real
    /// note: ratio is D1/(D1+D2)
    /// </summary>
    public class DropletSplitter: CompilerOperation
    {
        public string outDestName1 { get; }
        public string outDestName2 { get; }
        public string inDropletName { get; }
        public int outDest1X { get; }
        public int outDest1Y { get; }
        public int outDest2X { get; }
        public int outDest2Y { get; }
        public double ratio { get; }

        public int _order_id { get; }

        public DropletSplitter(string outDestName1, string outDestName2, string inDropletName, int outDest1X, int outDest1Y, int outDest2X, int outDest2Y, double ratio)
        {
            // TODO : add order_id here 
            this.outDestName1 = outDestName1;
            this.outDestName2 = outDestName2;
            this.inDropletName = inDropletName;
            this.outDest1X = outDest1X;
            this.outDest1Y = outDest1Y;
            this.outDest2X = outDest2X;
            this.outDest2Y = outDest2Y;
            this.ratio = ratio;
        }

        public void Executed()
        {
            //this.result1 = new Droplet(aimDroplet1, xValue1/2, yValue1, width, length, false);//
        }
    }
}
