using BioCompiler;
using Executor.Model;
using Executor;

namespace TestExecutor
{
    [TestClass]
    public class TestExecutor
    {

        // 0.25 ~ 0.75 mm

        Chip GetChipAndRun(string origin, int width, int length)
        {
            string content = new Runner().DoCompile(origin);
            ProcessExecutor executor = new ProcessExecutor();
            Chip c = new Chip(executor.GetOperationsListFromJSON(content), width, length);
            c.DoNextStep();
            return c;
        }


        [TestMethod]
        public void TestMove()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiMove()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n\r\n\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestInputAndOuput()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
         public void TestMultiInputAndOuput()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "input(d1,1,1,1.0);\r\n" + 
                "input(d2,4,4,0.5);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMerger()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
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
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        // d1,d2 ->d2
        [TestMethod]
        public void TestMixer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "mix(d1,2,2,2,2,5);\r\n\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiMixer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "mix(d1,2,2,2,2,5);\r\n\r\n" +
                "mix(d1,2,2,2,2,5);\r\n\r\n" +
                "mix(d1,2,2,2,2,5);\r\n\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }


        [TestMethod]
        public void TestSplitter()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,1,1,2.0);\r\n" +
                "split(d2,d3,d1,12,12,15,15,0.5);\r\n\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiSplitter()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,1,1,2.0);\r\n" +
                "split(d2,d3,d1,12,12,15,15,0.5);\r\n\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "split(d4,d5,d2,12,12,15,15,0.5);\r\n\r\n" +
                "output(d3,0,0);\r\n" +
                "output(d4,0,0);\r\n" +
                "output(d5,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestStorer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "store(d1,5,5,2.0);\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestMultiStorer()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "store(d1,5,5,2.0);\r\n" +
                "store(d1,5,5,2.0);\r\n" +
                "output(d1,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }

        [TestMethod]
        public void TestAll()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "input(d3,10,10,3.2);\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "split(d4,d5,d3,12,12,15,15,0.5);\r\n\r\n" +
                "merge(d3,d4,d5,5,9);\r\n\r\n" +
                "mix(d3,2,2,2,2,5);\r\n\r\n" +
                "store(d3,5,5,2.0);\r\n\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }


        [TestMethod]
         public void TestAllSpecialCase1()
        {
            // containing d1,d2-> d1
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "input(d3,10,10,3.2);\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n" +
                "droplet d4;\r\n" +
                "split(d4,d3,d3,12,12,15,15,0.5);\r\n" +
                "merge(d3,d4,d3,5,9);\r\n" +
                "mix(d3,2,2,2,2,5);\r\n" +
                "store(d3,5,5,2.0);\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";

            Chip c = GetChipAndRun(origin, 32, 20);
            Assert.AreEqual(true, c.manager.AllTasksCompleted());
        }
    }
}