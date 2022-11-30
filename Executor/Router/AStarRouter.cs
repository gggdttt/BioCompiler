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

        public void MoveOneStep(Droplet d, int destx, int desty, List<Droplet> activeDrplets, List<Droplet> busyDroplets)
        {
            List<IEdge> path = FindPath(d, destx, desty, activeDrplets, busyDroplets);
            if (path != null && path.Count() >= 1)
            {
                Droplet temp = busyDroplets.Where(t => t.name.Equals(d.name)).First();
                // has not complete move
                temp.xValue = (int)(path.First().End.Position.X - d.gridDiameter / 2);
                temp.yValue = (int)(path.First().End.Position.Y - d.gridDiameter / 2);
            }
            else
            {
                // has arrived or can not find path
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
            /*DisConnectAllNode(grid, d, activeDrplets, busyDroplets);*/

            int movingDropletCenterX = -1;
            int movingDropletCenterY = -1;
            if (d.gridDiameter % 2 != 0)
            {
                // could find a center cell
                if (d.gridDiameter <= 0) throw new Exception("Diameter is smaller than 0!");
                movingDropletCenterX = d.xValue + (int)(d.gridDiameter - 1) / 2;
                movingDropletCenterY = d.yValue + (int)(d.gridDiameter - 1) / 2;
            }
            else
            {
                // no center cell, choose the right bottom one as center cell.
                if (d.gridDiameter <= 0) throw new Exception("Diameter is smaller than 0!");
                movingDropletCenterX = d.xValue + (int)d.gridDiameter / 2;
                movingDropletCenterY = d.yValue + (int)d.gridDiameter / 2;
            }

            var pathFinder = new PathFinder();
            Console.WriteLine($"is trying to find path from ({movingDropletCenterX},{movingDropletCenterY}) to ({destx},{desty})");
            var path = pathFinder.FindPath(new GridPosition(movingDropletCenterX, movingDropletCenterY), new GridPosition(destx, desty), grid);

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
                    DisconnectOneNode(g, d, droplet);
                    Console.WriteLine("disconnect {0},{1}", d.xValue, d.yValue);
                }
            }
            // disconnect all the droplets in busyDroplets
            foreach (var droplet in busyDroplets)
            {
                if (!droplet.name.Equals(d.name))
                {
                    DisconnectOneNode(g, d, droplet);
                    Console.WriteLine("disconnect {0},{1}", d.xValue, d.yValue);
                }
            }
            // disconnect the area of boundray
            for (int i = 0; i < gridColumn; i++)
            {
                DisconnectOneNode(g, d, new Droplet("temp", i, 0, 1));
                DisconnectOneNode(g, d, new Droplet("temp", i, gridRow, 1));
            }
            for (int j = 0; j < gridColumn; j++)
            {
                DisconnectOneNode(g, d, new Droplet("temp", 0, j, 1));
                DisconnectOneNode(g, d, new Droplet("temp", gridColumn, j, 1));
            }
            return g;
        }

        public Grid DisconnectOneNode(Grid g, Droplet movingDroplet, Droplet blockedDroplet)
        {
            double rangeToDisconnect = 0;

            if (movingDroplet.gridDiameter % 2 != 0)
            {
                // could find a center cell
                double additionalDiameter = movingDroplet.gridDiameter - 1;
                if (additionalDiameter <= 0) throw new Exception("Diameter is smaller than 0!");
                rangeToDisconnect = (blockedDroplet.gridDiameter + additionalDiameter);
            }
            else
            {
                // no center cell, choose the right bottom one as center cell.
                double additionalDiameter = movingDroplet.gridDiameter;
                if (additionalDiameter <= 0) throw new Exception("Diameter is smaller than 0!");
                rangeToDisconnect = (blockedDroplet.gridDiameter + additionalDiameter);
            }

            for (int i = 0; i < rangeToDisconnect; i++)
                for (int j = 0; j < rangeToDisconnect; j++)
                {
                    Console.WriteLine();
                    g.DisconnectNode(new GridPosition(
                        Math.Min(gridColumn - 1, blockedDroplet.xValue + i),
                        Math.Min(gridRow - 1, blockedDroplet.yValue + j)));
                }
            return g;
        }
    }
}
