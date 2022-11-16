// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;

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

        private bool outputFlag;
        public DropletOutputer(string name, int xValue, int yValue, int line)
        {
            this.name = name;
            this.xValue = xValue;
            this.yValue = yValue;
            this.line = line;
            outputFlag = false;
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

            if (occupiedSet.Contains(name))
            {
                occupiedSet.Remove(name);
                declaredSet.Add(name);
                return true;
            }
            else return false;
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            if(activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            return false;
        }

        private void Active2Busy(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // Remove it from activeDroplets
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);
        }

        public void ExecuteOperation(List <Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        {
            List<Droplet> temp = busyDroplets.Where(droplet => droplet.name.Equals(name)).ToList();
            // if it has not moved to dest
            if (temp != null && (temp.First().xValue != xValue || temp.First().yValue != yValue))
            {
                manager.MoveByOneStep(temp.First(), xValue, yValue, activeDroplets, busyDroplets);
            }

            // check if it has moved to dest after the this movement
            // if it has moved to dest successfully, remove it from busy droplets and set flag to true
            if (temp != null && temp.First().xValue == xValue && temp.First().yValue == yValue)
            {

                busyDroplets.Remove(temp.First());
                outputFlag = true;
            }
            //int waitTime = Math.Abs(d1.xValue - xDest) + Math.Abs(d1.yValue - yDest);
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            // Return true if this droplet has been outputed
            return outputFlag;
        }

        public override string ToString()
        {
            return "DropletOutputer: " + name;
        }
    }
}
