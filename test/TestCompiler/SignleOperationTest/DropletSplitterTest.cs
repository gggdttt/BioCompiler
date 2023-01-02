using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletSplitterTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                      //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// Basic use of Splitter, d3->d1,d2
        /// </summary>
        [TestMethod]
        public void SplitterFunctionalityTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d3,10,10,0.1);\r\n" +
                "split(d1,d2,d3,12,12,15,15,0.5);";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 4,\r\n    \"name\": \"d3\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletSplitter, Executor\",\r\n    \"line\": 5,\r\n    \"outDestName1\": \"d1\",\r\n    \"outDestName2\": \"d2\",\r\n    \"inDropletName\": \"d3\",\r\n    \"outDest1X\": 12,\r\n    \"outDest1Y\": 12,\r\n    \"outDest2X\": 15,\r\n    \"outDest2Y\": 15,\r\n    \"ratio\": 0.5\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }


        /// <summary>
        /// Basic use of Splitter, d2->d1,d2
        /// </summary>
        [TestMethod]
        public void SplitterFunctionalityTest2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "input(d2,10,10,0.1);\r\n" +
                "split(d1,d2,d2,12,12,15,15,0.5);";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 3,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 10,\r\n    \"yValue\": 10,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletSplitter, Executor\",\r\n    \"line\": 4,\r\n    \"outDestName1\": \"d1\",\r\n    \"outDestName2\": \"d2\",\r\n    \"inDropletName\": \"d2\",\r\n    \"outDest1X\": 12,\r\n    \"outDest1Y\": 12,\r\n    \"outDest2X\": 15,\r\n    \"outDest2Y\": 15,\r\n    \"ratio\": 0.5\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// D3 is declared but not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void DropletDeclaredButNotAssignedValue()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "split(d1,d2,d3,12,12,15,15,0.5);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D2 is declared but not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotAssignedValueException))]
        public void DropletDeclaredButNotAssignedValue2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "split(d1,d2,d2,12,12,15,15,0.5);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D3 is not declared and not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclared()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "split(d1,d2,d3,12,12,15,15,0.5);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D3 is not declared and not input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclared2()
        {
            string origin =
                "droplet d1;\r\n" +
                "split(d1,d2,d2,12,12,15,15,0.5);";
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
                    "droplet d2;\r\n" +
                    "input(d2,10,10,0.1);\r\n" +
                    "splittttt(d1,d2,d2,12,12,15,15,0.5);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }


        /// <summary>
        /// D1 is occupied
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotReleasedException))]
        public void DropletNotReleased()
        {

            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d3,10,10,0.1);\r\n" +
                "split(d1,d2,d3,12,12,15,15,0.5);";
            new Runner().DoCompile(origin);

        }

    }
}
