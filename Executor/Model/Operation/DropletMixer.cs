// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using ToolSupporter.BioExceptions;

namespace Executor.Model.Operation
{
    /// <summary>
    /// mix(<droplet_name>,x_mix,y_mix,size_x,size_y,repeat_times)
    ///             string, int ,int ,int ,int int
    /// </summary>
    public class DropletMixer : CompilerOperation
    {

        public int line { get; }
        public string name { get; }
        public int xMix { get; }
        public int yMix { get; }
        public int xDistance { get; }
        public int yDistance { get; }
        public int repeatTimes { get; }

        private int currentRepeatTimes = 0;

        private bool movingBackFlag = false;
        // temp variable to store origin position of the droplet
        private int originX;
        private int originY;
        public DropletMixer(string name, int xMix, int yMix, int xDistance, int yDistance, int repeatTimes, int line)
        {

            this.name = name;
            this.xMix = xMix;
            this.yMix = yMix;
            this.xDistance = xDistance;
            this.yDistance = yDistance;
            this.repeatTimes = repeatTimes;
            this.line = line;
        }

        public int GetLine()
        {
            return line;
        }

        /// <summary>
        /// If its name is not in declaredSet, return 
        /// If it has been included in, return true and move it from declared Set to occupiedSet
        /// </summary>
        /// <param name="declaredSet"></param>
        /// <param name="occupiedSet"></param>
        public void DeclarationCheck(HashSet<string> declaredSet, HashSet<string> occupiedSet)
        {
            if(occupiedSet.Contains(name))
            {
                return;
            }    
            else if(declaredSet.Contains(name))
            {
                throw new VariableNotAssignedValueException(line);
            }
            else if(!occupiedSet.Contains(name)&&!declaredSet.Contains(name))
            {
                throw new DropletNotDeclaredException(line);
            }
        }

        public bool IsExecutable(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            if (activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1)
            {
                Active2Busy(activeDroplets, busyDroplets);
                return true;
            }
            return false;
        }

        private void Active2Busy(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            //droplet active->busy
            Droplet d1 = activeDroplets.Where(droplet => droplet.name.Equals(name)).First();
            activeDroplets.Remove(d1);
            busyDroplets.Add(d1);

            // save originX and originY
            originX = d1.xValue;
            originY = d1.yValue;
        }

        public void ExecuteOperation(List<Droplet> activeDroplets, List<Droplet> busyDroplets, MovementManager manager)
        {
            // droplet busy->active
            Droplet d1 = busyDroplets.Where(droplet => droplet.name.Equals(name)).First();

            // currentRepeatTimes
            if (d1.xValue == originX && d1.yValue == originY && currentRepeatTimes != repeatTimes)
            {
                currentRepeatTimes++;

                // now is now moving back
                movingBackFlag = false;

                // if now it is equal to required repeatTimes, do not need to move more
                if (currentRepeatTimes == repeatTimes)
                {
                    busyDroplets.Remove(d1);
                    activeDroplets.Add(d1);
                    return;
                }
            }

            if(d1.xValue == xMix && d1.yValue == yMix)
            {
                movingBackFlag = true;
            }

            if (!movingBackFlag)
            {// Moving from origin to dest
                manager.MoveByOneStep(d1, xMix, yMix, activeDroplets, busyDroplets);
            }
            else
            {
                manager.MoveByOneStep(d1, originX, originY, activeDroplets, busyDroplets);
            }
        }

        public bool HasExecuted(List<Droplet> activeDroplets, List<Droplet> busyDroplets)
        {
            return activeDroplets.Where(droplet => droplet.name.Equals(name)).Count() == 1
                && repeatTimes == currentRepeatTimes;
        }

        public override string ToString()
        {
            return "DropletMixer: " + name;
        }
    }
}
