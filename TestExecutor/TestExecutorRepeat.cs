using BioCompiler;
using Executor.Model;
using Executor;

namespace TestExecutor
{
    [TestClass]
    public class TestExecutorRepeat
    {

        Chip GetChipAndRun(string origin, int width, int length)
        {
            string content = new Runner().DoCompile(origin);
            ProcessExecutor executor = new ProcessExecutor();
            Chip c = new Chip(executor.GetOperationsListFromJSON(content), width, length);
            c.StartOpearions();
            return c;
        }


        [TestMethod]
        public void TestRunRepeat()
        {
            string origin =
                "# this is a demo\r\n\r\n" +
                "# droplet declaration\r\n" +
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "# droplet input\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d1,5,5);\r\n" +
                "move(d2,10,10);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestRunRepeatRecursive()
        {
            // TODO:
            // Now it fails
            string origin =
                "# this is a demo\r\n\r\n" +
                "# droplet declaration\r\n" +
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "# droplet input\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d1,5,5);\r\n" +
                "move(d2,10,10);\r\n" +
                "}\r\n\r\n" +
                "move(d1,5,5);\r\n" +
                "move(d2,10,10);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }


    }
}