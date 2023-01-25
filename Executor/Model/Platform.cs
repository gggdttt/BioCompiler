// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Model.Operation;


namespace Executor.Model
{

    public class Platform
    {

        int xLength;

        int yLength;

        public OperationManager operationManager { get; set; }

        public MovementManager movementManager { get; set; }

        public Platform(List<CompilerOperation> operations, int x, int y, string router)
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
            
            while (!operationManager.AllTasksCompleted())
            {
                operationManager.BeforeExecuting();
                operationManager.Executing(movementManager);
                operationManager.AfterExecute();

                movementManager.WriteCurrentRecordToFinalRecord();
            }
            return movementManager.GetFinalMovementRecord();
        }

    }
}
