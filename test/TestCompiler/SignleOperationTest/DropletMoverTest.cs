// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletMoverTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                      //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// Basic use of Moving(multiple droplet)
        /// </summary>
        [TestMethod]
        public void MoverFunctionalityTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "input(d3,10,10,0.1);\r\n\r\n" +
                "move(d1,3,3);\r\n" +
                "move(d2,7,7);\r\n" +
                "move(d3,9,9);\r\n";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 5,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 15,\r\n    \"yValue\": 15,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 6,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 7,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 9,\r\n    \"name\": \"d1\",\r\n    \"xDest\": 3,\r\n    \"yDest\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 10,\r\n    \"name\": \"d2\",\r\n    \"xDest\": 7,\r\n    \"yDest\": 7\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 11,\r\n    \"name\": \"d3\",\r\n    \"xDest\": 9,\r\n    \"yDest\": 9\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// Basic use of Moving(1 droplet)
        /// </summary>
        [TestMethod]
        public void MoverFunctionalityTest2()
        {
            string origin =
                "droplet d1;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "move(d1,3,3);\r\n";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 2,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 15,\r\n    \"yValue\": 15,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMover, Executor\",\r\n    \"line\": 3,\r\n    \"name\": \"d1\",\r\n    \"xDest\": 3,\r\n    \"yDest\": 3\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }


        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// D1 is declared but not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void DropletDeclaredButNotAssignedValue()
        {
            string origin =
                "droplet d1;\r\n" +
                "move(d1,3,3);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D1 is not declared and not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclared()
        {
            string origin = "move(d1,3,3);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// Key word error
        /// </summary>
        [TestMethod]
        public void KeyWordError()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetError(sw);
                string origin =
                    "droplet d1;\r\n" +
                    "input(d1,15,15,0.1);\r\n" +
                    "moveeeeeeee(d1,3,3);\r\n";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }
    }
}
