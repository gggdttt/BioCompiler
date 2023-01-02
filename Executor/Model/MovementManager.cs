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

        public string SETELIRecord { get; set; }
        public string CLRELIRecord { get; set; }

        public string FinalRecord { get; set; }
        int column { get; }
        int row { get; }

        public MovementManager(int columns, int rows, RouterOption option)
        {
            this.column = columns;
            this.row = rows;
            SETELIRecord = string.Empty;
            CLRELIRecord = string.Empty;
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
                int originGridIndex = result[0] + 1 + result[1] * column;
                int finalGridIndex = result[2] + 1 + result[3] * column;

                // find a path and move the droplet 
                SETELIRecord += $"  SETELI {originGridIndex};\r\n";
                SETELIRecord += $"  SETELI {finalGridIndex};\r\n";
                CLRELIRecord += $"  CLRELI {originGridIndex};\r\n";
                CLRELIRecord += $"  CLRELI {finalGridIndex};\r\n";
            }
        }
        public void ClearRecord()
        {
            SETELIRecord = string.Empty;
            CLRELIRecord = string.Empty;
        }

        public void WriteCurrentRecordToFinalRecord()
        {
            if (!string.IsNullOrEmpty(CLRELIRecord) && !string.IsNullOrEmpty(SETELIRecord))
            {
                // get record
                FinalRecord += SETELIRecord + "\r\n" + "TICK;\r\n";
                FinalRecord += CLRELIRecord + "\r\n" + "TICK;\r\n";
            }
            // clear record for next round
            ClearRecord();
        }

        public string GetFinalMovementRecord() { return FinalRecord + "  TSTOP;\r\n  TICK;\r\n  TICK; \r\n"; }
    }
}
