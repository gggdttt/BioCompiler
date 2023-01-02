// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using Executor.Router;
using BioCompiler;
using Executor.Model;
using Executor;
using Roy_T.AStar.Graphs;

namespace TestExecutor
{
    [TestClass]
    public class AstarFunctionalityTest
    {
        [TestMethod]
        public void TestAstarSingleLine()
        {
            AStarRouter astar = new AStarRouter(32, 20);
            string origin =
                    "droplet d1;\r\n" +
                    "input(d1,1,1,0.1);\r\n" +
                    "move(d1,3,1);\r\n" +
                    "output(d1,0,0);\r\n";
            string content = new Runner().DoCompile(origin);
            ProcessExecutor executor = new ProcessExecutor();
            Chip c = new Chip(executor.GetOperationsListFromJSON(content), 32, 20, "astar");
            c.StartOpearions();
            List<IEdge> path = astar.FindPath(new Droplet("d1", 1, 1, 0.1), 3, 1, c.operationManager.activeDroplets, c.operationManager.busyDroplets);
            Assert.IsNotNull(path);
        }

        [TestMethod]
        public void TestAstarSimpleRun()
        {
            string origin =
                    "droplet d1;\r\n" +
                    "input(d1,1,1,0.1);\r\n" +
                    "move(d1,3,1);\r\n" +
                    "output(d1,0,0);\r\n";
            string content = new Runner().DoCompile(origin);
            ProcessExecutor executor = new ProcessExecutor();
            Chip c = new Chip(executor.GetOperationsListFromJSON(content), 32, 20, "astar");
            c.StartOpearions();
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }
    }
}