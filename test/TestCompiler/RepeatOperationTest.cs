using BioCompiler;



namespace TestCompiler
{
    [TestClass]
    public class RepeatOperationTest
    {
        [TestMethod]
        public void CorrectInputTest1()
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
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 4\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 8,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 1,\r\n    \"yValue\": 1,\r\n    \"size\": 1.0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 9,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.5\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.RepeatOperation, Executor\",\r\n    \"line\": 11,\r\n    \"repeatTimes\": 10,\r\n    \"repeatOperations\": [\r\n      {\r\n        \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n        \"line\": 12,\r\n        \"name\": \"d1\",\r\n        \"xDest\": 3,\r\n        \"yDest\": 3\r\n      },\r\n      {\r\n        \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n        \"line\": 13,\r\n        \"name\": \"d2\",\r\n        \"xDest\": 7,\r\n        \"yDest\": 7\r\n      },\r\n      {\r\n        \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n        \"line\": 14,\r\n        \"name\": \"d1\",\r\n        \"xDest\": 5,\r\n        \"yDest\": 5\r\n      },\r\n      {\r\n        \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n        \"line\": 15,\r\n        \"name\": \"d2\",\r\n        \"xDest\": 10,\r\n        \"yDest\": 10\r\n      }\r\n    ]\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 19,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletOutputer, Executor\",\r\n    \"line\": 20,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 0,\r\n    \"yValue\": 0\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCheckRepeatWithUndeclaredDroplets()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d1,5,5);\r\n" +
                "move(d2,10,10);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
           new Runner().DoCompile(origin);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCheckRepeatError2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d3,7,7);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d2,0,0);\r\n\r\n";
            new Runner().DoCompile(origin);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestCheckRepeatError3()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n\r\n" +
                "repeat 10 times{\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "}\r\n\r\n" +
                "# output\r\n" +
                "output(d1,0,0);\r\n" +
                "output(d3,0,0);\r\n\r\n";
            new Runner().DoCompile(origin);
        }
    }
}