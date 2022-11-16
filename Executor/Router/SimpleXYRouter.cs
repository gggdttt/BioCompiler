
using Executor.Model;

namespace Executor.Router
{
    internal class SimpleXYRouter : RouterInter
    {
        int width;
        int height;

        public SimpleXYRouter(int width, int heights)
        {
            this.width = width;
            this.height = heights;
        }

        public void MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            if (!busyDroplets.Contains(d))
            {
                throw new ArgumentException("There is no such droplet in busy Droplets!");
            }

            if (CheckGrid(destx, desty))
            {
                throw new ArgumentException("The grid is out of bound!");
            }

            // move right
            if (d.xValue < destx && FindConflict(d, d.xValue + 1, d.yValue, activeDrplets, busyDroplets))
            {
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                temp.xValue++;
            }

            // move left
            else if (d.xValue > destx && FindConflict(d, d.xValue - 1, d.yValue, activeDrplets, busyDroplets))
            {
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                temp.xValue--;
            }

            // move up
            else if (d.yValue < desty && FindConflict(d, d.xValue, d.yValue + 1, activeDrplets, busyDroplets))
            {
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                temp.yValue++;
            }

            // move down
            else if (d.yValue > desty && FindConflict(d, d.xValue, d.yValue - 1, activeDrplets, busyDroplets))
            {
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                temp.yValue--;
            }

            else
            {
                // can not move or has arrived dest
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destx"></param>
        /// <param name="desty"></param>
        /// <param name="activeDrplets"></param>
        /// <param name="busyDroplets"></param>
        /// <returns>Find conflict or out of bound, return true, not find, return false</returns>
        private bool FindConflict(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            if (CheckGrid(destx, desty))
            {
                return true;
            }

            foreach (Droplet droplet in activeDrplets)
            {
                if (droplet.xValue == destx && droplet.yValue == desty)
                    return true;
            }

            foreach (Droplet droplet in busyDroplets)
            {
                if (droplet.xValue == destx && droplet.yValue == desty && !droplet.name.Equals(d.name))
                    return true;
            }
            return false;
        }


        private bool CheckGrid(int destx, int desty)
        {
            return 0 <= destx && 0 <= desty && destx < width && desty < height;
        }

    }
}
