using Executor.Model;

namespace Executor.Router
{
    public interface RouterInterface
    {
        /// <summary>
        /// You can implement any router as you want. 
        /// What you need to implement is: For a droplet, how to move it for every one step
        /// You can try to find a whole path according to your algorithm and then get the first step from found path.
        /// Of course, the best solution is just finding one step instead of searching for the whole path.
        /// </summary>
        /// <param name="d">Droplet will move</param>
        /// <param name="destx">The final destination of droplet</param>
        /// <param name="desty">The final destination of droplet</param>
        /// <param name="activeDrplets">Current active droplet</param>
        /// <param name="busyDroplets">The current busy droplets</param>
        public int[] MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets);
    }
}
