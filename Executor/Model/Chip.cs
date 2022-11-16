using Executor.Model.Operation;


namespace Executor.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Chip
    {

        int xLength;

        int yLength;

        public OperationManager operationManager { get; set; }

        public MovementManager movementManager { get; set; }

        public Chip(List<CompilerOperation> operations, int x, int y)
        {
            this.xLength = x;
            this.yLength = y;
            operationManager = new OperationManager(operations, x, y);
            // TODO support init option
            movementManager = new MovementManager(x, y, Router.RouterOption.SimpleXY);
        }

        public void StartOpearions()
        {
            int i = 0;
            while (!operationManager.AllTasksCompleted() && i < 100)
            {
                operationManager.BeforeExecuting();
                operationManager.Executing(movementManager);
                operationManager.AfterExecute();
                i++;
                Console.WriteLine("i=" + i);
            }
            Console.WriteLine("Executing completed!");
        }

    }
}
