// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using ToolSupporter.BioExceptions;

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

        private bool firstTimeFlag = true;

        public DropletSplitter(string outDestName1, string outDestName2, string inDropletName, int outDest1X, int outDest1Y, int outDest2X, int outDest2Y, double ratio, int line)
        {
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
        public void DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
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
                return;
            }

            else if (occupiedSet.Contains(outDestName1)
                && declaredSet.Contains(outDestName2)
                && occupiedSet.Contains(inDropletName)
                && outDestName1.Equals(inDropletName))
            {
                declaredSet.Remove(outDestName2);
                occupiedSet.Add(outDestName2);
                return;
            }

            else if (declaredSet.Contains(outDestName1)
                && occupiedSet.Contains(outDestName2)
                && occupiedSet.Contains(inDropletName)
                && outDestName2.Equals(inDropletName))
            {
                declaredSet.Remove(outDestName1);
                occupiedSet.Add(outDestName1);
                return;
            }
            else if (!occupiedSet.Contains(inDropletName) && !declaredSet.Contains(inDropletName)
                || !occupiedSet.Contains(outDestName1) && !declaredSet.Contains(outDestName1)
                || !occupiedSet.Contains(outDestName2) && !declaredSet.Contains(outDestName2))
            {
                throw new DropletNotDeclaredException(line);
            }
            else if (!occupiedSet.Contains(inDropletName))
            { 
                throw new VariableNotAssignedValueException(line); 
            }
            else { throw new VariableNotReleasedException(line); }
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // special case1: d1 -> d1 d2
            if (inDropletName.Equals(outDestName1)
                && !inDropletName.Equals(outDestName2)
                && activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).Count() == 1
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName2)).Count() == 0)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            // special case2: d1-> d2 d1
            if (inDropletName.Equals(outDestName2)
                && !inDropletName.Equals(outDestName1)
                && activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).Count() == 1
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName1)).Count() == 0)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }

            // the indroplet exist and available and two output droplet not busy
            // the out droplet1 and outdroplet2 have not been inputed 
            if (activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).Count() == 1
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName1)).Count() == 0
                && activeDroplets.Where(droplet => droplet.name.Equals(outDestName2)).Count() == 0)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            return false;
        }

        private void Active2Busy(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // d1: active -> busy
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(inDropletName)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);
        }

        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        {
            if (firstTimeFlag && CheckSplittable(activeDroplets, busyDroplets))
            {// the first time to run
                List<Droplet> l1 = busyDroplets.Where(droplet => droplet.name.Equals(inDropletName)).ToList();
                busyDroplets.Remove(l1.First());
                // TODO: need to check the position
                busyDroplets.Add(new Droplet(outDestName1, l1.First().xValue, l1.First().yValue, l1.First().volume * ratio));
                busyDroplets.Add(new Droplet(outDestName2, l1.First().xValue + 1, l1.First().yValue, l1.First().volume * (1 - ratio)));
                firstTimeFlag = false;
                return;
            }

            if (firstTimeFlag && !CheckSplittable(activeDroplets, busyDroplets))
            {// now the droplet is not splittable, wait
                return;
            }

            // add two new generated droplet to active Droplets
            Droplet droplet1 = busyDroplets.Where(droplet => droplet.name.Equals(outDestName1)).First();
            Droplet droplet2 = busyDroplets.Where(droplet => droplet.name.Equals(outDestName2)).First();
            if (droplet1.xValue != outDest1X || droplet1.yValue != outDest1Y)
            {
                manager.MoveByOneStep(droplet1, outDest1X, outDest1Y, activeDroplets, busyDroplets);
            }
            if (droplet2.xValue != outDest2X || droplet2.yValue != outDest2Y)
            {
                manager.MoveByOneStep(droplet2, outDest2X, outDest2Y, activeDroplets, busyDroplets);
            }
            if (droplet1.xValue == outDest1X && droplet1.yValue == outDest1Y
                && droplet2.xValue == outDest2X && droplet2.yValue == outDest2Y)
            {// have arrived in the dest
                busyDroplets.Remove(droplet1);
                busyDroplets.Remove(droplet2);
                activeDroplets.Add(droplet1);
                activeDroplets.Add(droplet2);
            }

        }
        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            List<Droplet> l1 = activeDroplets.Where(droplet => droplet.name.Equals(outDestName1)).ToList();
            List<Droplet> l2 = activeDroplets.Where(droplet => droplet.name.Equals(outDestName2)).ToList();
            return
                l1 != null
                && l2 != null
                && l1!.Count() == 1
                && l2!.Count() == 1
                && l1.First().xValue == outDest1X
                && l1.First().yValue == outDest1Y
                && l2.First().xValue == outDest2X
                && l2.First().yValue == outDest2Y;
        }

        public override string ToString()
        {
            return "DropletSplitter: " + " outDestName1:" + outDestName1 + " outDestName2:" + outDestName2 + " inDropletName:" + inDropletName;
        }

        private bool CheckSplittable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // TODO: implement rule here 
            return true;
        }
    }
}
