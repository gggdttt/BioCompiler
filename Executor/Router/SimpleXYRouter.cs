// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Model;

namespace Executor.Router
{
    /// <summary>
    /// This is the demo XY Router, but I do not test its boundry. Just a demo to show how to add a new Router.
    /// </summary>
    internal class SimpleXYRouter : RouterInterface
    {
        int width;
        int height;

        public SimpleXYRouter(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int[] MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            int[] result = new int[4];
            result[0] = d.xValue;
            result[1] = d.yValue;
            if (!busyDroplets.Contains(d))
            {
                throw new ArgumentException("There is no such droplet in busy Droplets!");
            }

            if (!CheckGrid(destx, desty))
            {
                Console.WriteLine("x is :" + destx + ", y is:" + desty);
                Console.WriteLine("width is :" + width + ", height is:" + height);
                throw new ArgumentException("The grid is out of bound!");
            }

            Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
            // move right
            if (d.xValue < destx && FindConflict(d, d.xValue + 1, d.yValue, activeDrplets, busyDroplets))
            {
                temp.xValue++;
            }
            // move left
            else if (d.xValue > destx && FindConflict(d, d.xValue - 1, d.yValue, activeDrplets, busyDroplets))
            {
                temp.xValue--;
            }
            // move up
            else if (d.yValue < desty && FindConflict(d, d.xValue, d.yValue + 1, activeDrplets, busyDroplets))
            {
                temp.yValue++;
            }
            // move down
            else if (d.yValue > desty && FindConflict(d, d.xValue, d.yValue - 1, activeDrplets, busyDroplets))
            {
                temp.yValue--;
            }
            else
            {
                // can not move or has arrived dest
                return null;
            }
            result[2] = temp.xValue;
            result[3] = temp.yValue;
            return result;
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
