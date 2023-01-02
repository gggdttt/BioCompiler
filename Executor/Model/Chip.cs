using Executor.Model.Operation;


namespace Executor.Model
{

    public class Chip
    {

        int xLength;

        int yLength;

        public OperationManager operationManager { get; set; }

        public MovementManager movementManager { get; set; }

        public Chip(List<CompilerOperation> operations, int x, int y, string router)
        {
            this.xLength = x;
            this.yLength = y;
            operationManager = new OperationManager(operations, x, y);

            if (router.ToLower().Equals("astar"))
            {
                movementManager = new MovementManager(x, y, Router.RouterOption.AStar);
            }
            else if (router.ToLower().Equals("simplexy"))
            {
                movementManager = new MovementManager(x, y, Router.RouterOption.SimpleXY);
            }
            else
            {
                // default is AStar
                movementManager = new MovementManager(x, y, Router.RouterOption.AStar);
            }
        }

        public string StartOpearions()
        {
            string result = string.Empty;
            while (!operationManager.AllTasksCompleted())
            {
                operationManager.BeforeExecuting();
                operationManager.Executing(movementManager);
                operationManager.AfterExecute();

                if (!string.IsNullOrEmpty(movementManager.CLRELIRecord) && !string.IsNullOrEmpty(movementManager.SETELIRecord))
                {
                    // get record
                    result += movementManager.SETELIRecord + "\r\n" + "TICK;\r\n";
                    result += movementManager.CLRELIRecord + "\r\n" + "TICK;\r\n";
                }
                // clear record for next round
                movementManager.ClearRecord();
            }
            return result + "  TSTOP;\r\n  TICK;\r\n  TICK; \r\n";
        }

    }
}
