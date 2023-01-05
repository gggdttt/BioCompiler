// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Router;

namespace Executor.Model
{
    public class MovementManager
    {

        RouterInterface router { get; }

        /// <summary>
        /// Every movement can be recorded with 3 steps: 
        /// 1.enable the current cell where the droplet is on
        /// 2. enable the current cell and its next cell to move
        /// 3. disable the previous cell
        /// 4. disable current cell
        /// </summary>
        public string FirstStepRecord { get; set; }
        public string SecondStepRecord { get; set; }
        public string ThirdStepRecord { get; set; }

        public string FinalRecord { get; set; }
        int column { get; }
        int row { get; }

        public MovementManager(int columns, int rows, RouterOption option)
        {
            this.column = columns;
            this.row = rows;
            FirstStepRecord = string.Empty;
            SecondStepRecord = string.Empty;
            ThirdStepRecord = string.Empty;
            FinalRecord = string.Empty;
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
            int[] result = this.router!.MoveOneStep(d, destx, desty, activeDrplets, busyDroplets);
            if (result != null)
            {
                WriteDropletMovementToRoundRecord(result[0], result[1], result[2], result[3], d.gridDiameter);
            }
        }


        /// <summary>
        /// Clear all the records
        /// </summary>
        private void ClearRecord()
        {
            FirstStepRecord = string.Empty;
            SecondStepRecord = string.Empty;
            ThirdStepRecord = string.Empty;
        }

        public void WriteCurrentRecordToFinalRecord()
        {

            // get record
            FinalRecord += FirstStepRecord + "\r\n  TICK;\r\n";
            FinalRecord += SecondStepRecord + "\r\n  TICK;\r\n";
            FinalRecord += ThirdStepRecord + "\r\n  TICK;\r\n";

            // clear record for next round
            ClearRecord();
        }

        private void WriteDropletMovementToRoundRecord(int originX, int originY, int finalX, int finalY, int gridDiameter)
        {

            for (int i = 0; i < gridDiameter; i++)
            {
                for (int j = 0; j < gridDiameter; j++)
                {
                    int originGridIndex = (originX + i) + 1 + (originY + j) * column;
                    int finalGridIndex = (finalX + i) + 1 + (finalY + j) * column;
                    FirstStepRecord += $"  SETELI {originGridIndex};\r\n";
                    SecondStepRecord += $"  SETELI {finalGridIndex};\r\n";
                    if (!(originX + i >= finalX && originX + i < finalX + gridDiameter
                        && originY + j >= finalY && originY + j < finalY + gridDiameter))
                    {
                        // not clear all,
                        ThirdStepRecord += $"  CLRELI {originGridIndex};\r\n";
                    }
                }
            }
        }

        public string GetFinalMovementRecord()
        {
            return FinalRecord + "  TSTOP;\r\n  TICK;\r\n  TICK; \r\n";
        }
    }
}
