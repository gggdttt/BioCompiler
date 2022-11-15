using Newtonsoft.Json;
using System.Text;
using BioCompiler;
using BioCompiler.Compiler;
using System.Collections.Immutable;


namespace TestCompiler
{
    [TestClass]
    public class FullTest
    {
        /// <summary>
        /// The normal input without any special status
        /// </summary>
        [TestMethod]
        public void FullTestWithoutRepeat1()
        {
            string origin =
                "# this is a demo\r\n" +
                "\r\n" +
                "# droplet declaration\r\n" +
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "\r\n" +
                "# droplet input\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "input(d3,10,10,3.2);\r\n" +
                "\r\n" +
                "# move\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n" +
                "\r\n" +
                "# split \r\n" +
                "# d3-> d4, d5\r\n" +
                "droplet d4;\r\n" +
                "droplet d5;\r\n" +
                "split(d4,d5,d3,12,12,15,15,0.5);\r\n" +
                "\r\n" +
                "# merging\r\n" +
                "# d4,d5->d3\r\n" +
                "merge(d3,d4,d5,5,9);\r\n" +
                "\r\n" +
                "# mixing\r\n" +
                "mix(d3,2,2,2,2,5);\r\n" +
                "\r\n" +
                "# store\r\n" +
                "store(d3,5,5,2.0);\r\n" +
                "\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);\r\n";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 4\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 6\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 9,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 1,\r\n    \"yValue\": 1,\r\n    \"size\": 1.0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 10,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 11,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 3.2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 14,\r\n    \"name\": \"d1\",\r\n    \"xDest\": 3,\r\n    \"yDest\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 15,\r\n    \"name\": \"d2\",\r\n    \"xDest\": 7,\r\n    \"yDest\": 7\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 16,\r\n    \"name\": \"d3\",\r\n    \"xDest\": 9,\r\n    \"yDest\": 9\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d4\",\r\n    \"line\": 20\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d5\",\r\n    \"line\": 21\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletSplitter, Executor\",\r\n    \"line\": 22,\r\n    \"outDestName1\": \"d4\",\r\n    \"outDestName2\": \"d5\",\r\n    \"inDropletName\": \"d3\",\r\n    \"outDest1X\": 12,\r\n    \"outDest1Y\": 12,\r\n    \"outDest2X\": 15,\r\n    \"outDest2Y\": 15,\r\n    \"ratio\": 0.5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMerger, Executor\",\r\n    \"line\": 26,\r\n    \"outDropletName\": \"d3\",\r\n    \"inDroplet1Name\": \"d4\",\r\n    \"inDroplet2Name\": \"d5\",\r\n    \"xDest\": 5,\r\n    \"yDest\": 9\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMixer, Executor\",\r\n    \"line\": 29,\r\n    \"name\": \"d3\",\r\n    \"xMix\": 2,\r\n    \"yMix\": 2,\r\n    \"xDistance\": 2,\r\n    \"yDistance\": 2,\r\n    \"repeatTimes\": 5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletStorer, Executor\",\r\n    \"line\": 32,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 5,\r\n    \"yValue\": 5,\r\n    \"latency\": 2.0,\r\n    \"time\": 0.0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 35,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 36,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 37,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// include merge d1,d2 -> d1 and split d1-> d1,d2
        /// </summary>
        [TestMethod]
        public void FullTestWithoutRepeat2()
        {
            string origin = 
                "# this is a demo\r\n" +
                "\r\n" +
                "# droplet declaration\r\n" +
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "\r\n" +
                "# droplet input\r\n" +
                "input(d1,1,1,1.0);\r\n" +
                "input(d2,4,4,0.5);\r\n" +
                "input(d3,10,10,3.2);\r\n" +
                "\r\n" +
                "# move\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n" +
                "\r\n" +
                "# split \r\n" +
                "# d3-> d3, d4\r\n" +
                "droplet d4;\r\n" +
                "split(d3,d4,d3,12,12,15,15,0.5);\r\n" +
                "\r\n" +
                "# merging\r\n" +
                "# d3,d4->d3\r\n" +
                "merge(d3,d4,d3,5,9);\r\n" +
                "\r\n" +
                "# mixing\r\n" +
                "mix(d3,2,2,2,2,5);\r\n" +
                "\r\n" +
                "# store\r\n" +
                "store(d3,5,5,2.0);\r\n" +
                "\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n" +
                "output(d3,0,0);";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 4\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 6\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 9,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 1,\r\n    \"yValue\": 1,\r\n    \"size\": 1.0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 10,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 11,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 3.2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 14,\r\n    \"name\": \"d1\",\r\n    \"xDest\": 3,\r\n    \"yDest\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 15,\r\n    \"name\": \"d2\",\r\n    \"xDest\": 7,\r\n    \"yDest\": 7\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 16,\r\n    \"name\": \"d3\",\r\n    \"xDest\": 9,\r\n    \"yDest\": 9\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d4\",\r\n    \"line\": 20\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletSplitter, Executor\",\r\n    \"line\": 21,\r\n    \"outDestName1\": \"d3\",\r\n    \"outDestName2\": \"d4\",\r\n    \"inDropletName\": \"d3\",\r\n    \"outDest1X\": 12,\r\n    \"outDest1Y\": 12,\r\n    \"outDest2X\": 15,\r\n    \"outDest2Y\": 15,\r\n    \"ratio\": 0.5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMerger, Executor\",\r\n    \"line\": 25,\r\n    \"outDropletName\": \"d3\",\r\n    \"inDroplet1Name\": \"d4\",\r\n    \"inDroplet2Name\": \"d3\",\r\n    \"xDest\": 5,\r\n    \"yDest\": 9\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMixer, Executor\",\r\n    \"line\": 28,\r\n    \"name\": \"d3\",\r\n    \"xMix\": 2,\r\n    \"yMix\": 2,\r\n    \"xDistance\": 2,\r\n    \"yDistance\": 2,\r\n    \"repeatTimes\": 5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletStorer, Executor\",\r\n    \"line\": 31,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 5,\r\n    \"yValue\": 5,\r\n    \"latency\": 2.0,\r\n    \"time\": 0.0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 34,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 35,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 36,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void TestWithRepeat()
        {
            // TODO: Add test for repeat
        }
    }
}
