using Executor.Router;

namespace Executor.Model
{
    public class MovementManager
    {

        RouterInter router { get; }

        public MovementManager(int width, int height, RouterOption option)
        { 
            switch (option)
            {
                case RouterOption.SimpleXY:
                    {
                        router = new SimpleXYRouter(width, height);
                        break;
                    }
                case RouterOption.ConflictBased:
                    {
                        throw new NotImplementedException();
                    }
                case RouterOption.AStar:
                    {
                        throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }
        }
        public void MoveByOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            this.router!.MoveOneStep(d, destx, desty, activeDrplets, busyDroplets);

        }


    }
}
