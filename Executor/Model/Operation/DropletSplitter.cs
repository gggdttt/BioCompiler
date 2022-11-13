// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Collections.Immutable;
using System.Xml.Linq;

namespace Executor.Model.Operation
{
    /// <summary>
    /// split(<out_dest_name1>,<out_dest_name2>,<in_droplet_name>,left_x_dest, left_y_dest, right_x_dest, right_y_dest, ratio);
    /// string, string, strig, int, int ,int, int, real
    /// note: ratio is D1/(D1+D2)
    /// </summary>
    public class DropletSplitter : CompilerOperation
    {
        public int line { get; }
        public string outDestName1 { get; }
        public string outDestName2 { get; }
        public string inDropletName { get; }
        public int outDest1X { get; }
        public int outDest1Y { get; }
        public int outDest2X { get; }
        public int outDest2Y { get; }
        public double ratio { get; }

        public int _order_id { get; }

        public DropletSplitter(string outDestName1, string outDestName2, string inDropletName, int outDest1X, int outDest1Y, int outDest2X, int outDest2Y, double ratio, int line)
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
            this.line = line;
        }

        public int GetLine()
        {
            return line;
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
            if (declaredSet.Contains(outDestName1)
                && declaredSet.Contains(outDestName2)
                && occupiedSet.Contains(inDropletName))
            {
                declaredSet.Remove(outDestName1);
                occupiedSet.Add(outDestName1);
                declaredSet.Remove(outDestName2);
                occupiedSet.Add(outDestName2);

                occupiedSet.Remove(inDropletName);
                declaredSet.Add(inDropletName);
                return true;
            }

            else if (occupiedSet.Contains(outDestName1)
                && declaredSet.Contains(outDestName2)
                && occupiedSet.Contains(inDropletName)
                && outDestName1.Equals(inDropletName))
            {
                declaredSet.Remove(outDestName2);
                occupiedSet.Add(outDestName2);
                return true;
            }

            else if (declaredSet.Contains(outDestName1)
                && occupiedSet.Contains(outDestName2)
                && occupiedSet.Contains(inDropletName)
                && outDestName2.Equals(inDropletName))
            {
                declaredSet.Remove(outDestName1);
                occupiedSet.Add(outDestName1);
                return true;
            }
            else return false;
        }

        public bool IsExecutable(ImmutableList<Droplet> activeDroplets)
        {
            // the indroplet exist
            // the out droplet1 and outdroplet2 have not been inputed 
            return activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).Count() == 1
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName1)).Count() == 0
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName2)).Count() == 0;
        }

        public void Active2Busy(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            // d1: active -> busy
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);
        }

        public void ExecuteOperation(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            // add two new generated droplet to active Droplets
            Droplet d1 = busyDroplets.Where(droplet => droplet.name.Equals(inDropletName)).First();
            busyDroplets.Remove(d1);
            activeDroplets.Add(new Droplet(outDestName1, outDest1X, outDest1Y, d1.size * ratio));
            activeDroplets.Add(new Droplet(outDestName2, outDest2X, outDest2Y, d1.size * (1 - ratio)));
        }
        public bool HasExecuted(ImmutableList<Droplet> activeDroplets, ImmutableList<Droplet> busyDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).Count() == 0
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName1)).Count() == 1
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName2)).Count() == 1;
        }
    }
}
