// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using BioCompiler;
using Executor.Model;
using Executor;

namespace TestExecutor
{
    [TestClass]
    public class ExecutorFunctionalityTest
    {

        // 0.25 ~ 0.75 mm

        Chip GetChipAndRun(string origin, int width, int length)
        {
            string content = new Runner().DoCompile(origin);
            ProcessExecutor executor = new ProcessExecutor();
            Chip c = new Chip(executor.GetOperationsListFromJSON(content), width, length, "astar");
            c.StartOpearions();
            return c;
        }


        [TestMethod]
        public void TestMove()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiMove()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestInputAndOuput()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiInputAndOuput()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,10,10,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "input(d1,10,10,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMerger()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,10,10,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiMerger()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "input(d3,10,10,3.2);\r\n\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "merge(d4,d1,d2,5,9);\r\n\r\n" +
                "merge(d5,d4,d3,5,9);\r\n\r\n" +
                "output(d5,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        // d1,d2 ->d2
        [TestMethod]
        public void TestMixer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "mix(d1,3,3,3,3,5);\r\n\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiMixer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,0.1);\r\n" +
                "mix(d1,2,2,2,2,5);\r\n" +
                "mix(d1,2,2,2,2,5);\r\n" +
                "mix(d1,2,2,2,2,5);\r\n" +
                "output(d1,0,0);\r\n";
            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestSplitter()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,1,1,0.1);\r\n" +
                "split(d2,d3,d1,10,10,20,10,0.5);\r\n\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiSplitter()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,1,1,0.1);\r\n" +
                "split(d2,d3,d1,12,12,15,15,0.5);\r\n\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "split(d4,d5,d2,12,12,15,15,0.5);\r\n\r\n" +
                "output(d3,0,0);\r\n" +
                "output(d4,0,0);\r\n" +
                "output(d5,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestStorer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "store(d1,5,5,0.5);\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiStorer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "store(d1,5,5,0.5);\r\n" +
                "store(d1,5,5,1.0);\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestAll()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "input(d3,10,10,0.05);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "split(d4,d5,d3,12,12,15,15,0.5);\r\n\r\n" +
                "merge(d3,d4,d5,5,9);\r\n\r\n" +
                "mix(d3,2,2,2,2,5);\r\n\r\n" +
                "store(d3,5,5,1.0);\r\n\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }


        [TestMethod]
        public void TestAllSpecialCase1()
        {
            // containing d1,d2-> d1
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "input(d3,10,10,0.1);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n" +
                "droplet d4;\r\n" +
                "split(d4,d3,d3,12,12,15,15,0.5);\r\n" +
                "merge(d3,d4,d3,5,9);\r\n" +
                "mix(d3,2,2,2,2,5);\r\n" +
                "store(d3,5,5,0.5);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
        }


        [TestMethod]
        public void TestAllSpecialCase2()
        {
            // input again after output
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n" +
                "input(d3,10,10,0.1);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n" +
                "droplet d4;\r\n" +
                "split(d4,d3,d3,12,12,15,15,0.5);\r\n" +
                "merge(d3,d4,d3,5,9);\r\n" +
                "mix(d3,2,2,2,2,5);\r\n" +
                "store(d3,5,5,1.0);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n" +
                "input(d3,10,10,3.2);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.operationManager.AllTasksCompleted());
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
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n\r\n" +
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
                "input(d1,1,1,0.05);\r\n" +
                "input(d2,4,4,0.05);\r\n\r\n" +
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