// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)

using Roy_T.AStar.Grids;
using Roy_T.AStar.Primitives;
using Roy_T.AStar.Paths;
using Executor.Model;
using Roy_T.AStar.Graphs;

namespace Executor.Router
{

    public class AStarRouter : RouterInterface
    {

        public int gridColumn { get; set; }
        public int gridRow { get; set; }

        public AStarRouter(int columns, int rows)
        {
            this.gridColumn = columns;
            this.gridRow = rows;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="destx"></param>
        /// <param name="desty"></param>
        /// <param name="activeDrplets"></param>
        /// <param name="busyDroplets"></param>
        /// <returns></returns>
        public int[] MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            int[] result = new int[4];
            // record origin position
            result[0] = d.xValue;
            result[1] = d.yValue;
            List<IEdge> path = FindPath(d, destx, desty, activeDrplets, busyDroplets);
            if (path != null && path.Count() >= 1)
            {
                
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                // has not complete move
                temp.xValue = (int)(path.First().End.Position.X);
                temp.yValue = (int)(path.First().End.Position.Y);
                result[2] = temp.xValue;
                result[3] = temp.yValue;
                return result;
            }
            else
            {
                // has arrived or can not find path
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="destx"></param>
        /// <param name="desty"></param>
        /// <param name="activeDrplets"></param>
        /// <param name="busyDroplets"></param>
        public List<IEdge> FindPath(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            //init 
            var gridSize = new GridSize(columns: this.gridColumn, rows: this.gridRow);
            var cellSize = new Size(Distance.FromMeters(1), Distance.FromMeters(1));
            var traversalVelocity = Velocity.FromMetersPerSecond(1);

            // Create a new grid, each cell is laterally connected (like how a rook moves over a chess board, other options are available)
            var grid = Grid.CreateGridWithLateralConnections(gridSize, cellSize, traversalVelocity);
            DisConnectAllNode(grid, d, activeDrplets, busyDroplets);

            var pathFinder = new PathFinder();
            Console.WriteLine($"is trying to find path from ({d.xValue},{d.yValue}) to ({destx},{desty})");
            var path = pathFinder.FindPath(new GridPosition(d.xValue, d.yValue), new GridPosition(destx, desty), grid);

            Console.WriteLine($"type: {path.Type}, distance: {path.Distance}, duration {path.Duration}");

            // Use path.Edges to get the actual path
            return path.Edges.ToList();
        }

        public Grid DisConnectAllNode(Grid g, Droplet d, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {

            // disconnect all the droplets in activeDroplets
            foreach (var droplet in activeDrplets)
            {
                if (!droplet.name.Equals(d.name))
                {
                    DisconnectOneDroplet(g, d, droplet);
                    Console.WriteLine("disconnect {0},{1}", d.xValue, d.yValue);
                }
            }
            // disconnect all the droplets in busyDroplets
            foreach (var droplet in busyDroplets)
            {
                if (!droplet.name.Equals(d.name))
                {
                    DisconnectOneDroplet(g, d, droplet);
                    Console.WriteLine("disconnect {0},{1}", d.xValue, d.yValue);
                }
            }
            // disconnect the area of boundray
            DisconnectBoundry(g, d);
            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="movingDroplet"></param>
        /// <returns></returns>
        public Grid DisconnectBoundry(Grid g, Droplet movingDroplet)
        {

            for (int i = 0; i < gridColumn; i++)
                for (int j = gridRow - movingDroplet.gridDiameter; j < gridRow; j++)
                {
                    g.DisconnectNode(new GridPosition(i, j));
                }
            for (int i = gridColumn - movingDroplet.gridDiameter; i < gridColumn; i++)
                for (int j = 0; j < gridRow; j++)
                {
                    g.DisconnectNode(new GridPosition(i, j));
                }
            return g;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="movingDroplet"></param>
        /// <param name="blockedDroplet"></param>
        /// <returns></returns>
        public Grid DisconnectOneDroplet(Grid g, Droplet movingDroplet, Droplet blockedDroplet)
        {
            // two droplet need at least one grid distance!!!
            int newTopLeftPointX = blockedDroplet.xValue - movingDroplet.gridDiameter - 1;
            int newTopLeftPointY = blockedDroplet.yValue - movingDroplet.gridDiameter - 1;

            // two droplet need at least one grid distance!!!
            for (int i = newTopLeftPointX; i < blockedDroplet.xValue + blockedDroplet.gridDiameter + 1; i++)
                for (int j = newTopLeftPointY; j < blockedDroplet.xValue + blockedDroplet.gridDiameter + 1; j++)
                {
                    /*                    
                     *                  ********        *******0
                                        ********   ->   ****00*0
                                        **00*0**        **0*00*0
                                        **00****        *******0
                                        ********        00000000*/
                    g.DisconnectNode(new GridPosition(
                        Math.Max(0, Math.Min(gridColumn - 1, i)),
                        Math.Max(0, Math.Min(gridRow - 1, j))));
                }
            return g;
        }
    }
}
