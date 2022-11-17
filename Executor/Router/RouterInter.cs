using Executor.Model;

namespace Executor.Router
{
    public  interface RouterInter
    {
        public void MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets);

    }
}
