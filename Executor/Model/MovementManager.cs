using Executor.Router;

namespace Executor.Model
{
    public class MovementManager
    {

        RouterInterface router { get; }

        public MovementManager(int columns, int rows, RouterOption option)
        { 
            switch (option)
            {
                case RouterOption.SimpleXY:
                    {
                        router = new SimpleXYRouter(columns, rows);
                        break;
                    }
                case RouterOption.ConflictBased:
                    {
                        throw new NotImplementedException();
                    }
                case RouterOption.AStar:
                    {
                        router = new AStarRouter(columns, rows);
                        break;
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
