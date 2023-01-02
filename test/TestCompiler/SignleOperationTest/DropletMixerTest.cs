using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletMixerTest
    {

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                      //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// Basic use of input
        /// </summary>
        [TestMethod]
        public void FunctionTest()
        {
            string origin = 
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n\r\n" +
                "merge(d3,d1,d2,5,9);\r\n\r\n" +
                "mix(d3,2,2,2,2,5);";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 5,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 15,\r\n    \"yValue\": 15,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 6,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMerger, Executor\",\r\n    \"line\": 8,\r\n    \"outDropletName\": \"d3\",\r\n    \"inDroplet1Name\": \"d1\",\r\n    \"inDroplet2Name\": \"d2\",\r\n    \"xDest\": 5,\r\n    \"yDest\": 9\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMixer, Executor\",\r\n    \"line\": 10,\r\n    \"name\": \"d3\",\r\n    \"xMix\": 2,\r\n    \"yMix\": 2,\r\n    \"xDistance\": 2,\r\n    \"yDistance\": 2,\r\n    \"repeatTimes\": 5\r\n  }\r\n]";
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
                "droplet d3;\r\n" +
                "mix(d3,2,2,2,2,5);";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D3 is not declraed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void DropletNotDeclared()
        {
            string origin =
                "mix(d3,2,2,2,2,5);";
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
                    "droplet d3;\r\n\r\n" +
                    "input(d1,15,15,0.1);\r\n" +
                    "input(d2,4,4,0.1);\r\n\r\n" +
                    "merge(d3,d1,d2,5,9);\r\n\r\n" +
                    "mixxxxx(d3,2,2,2,2,5);";
                new Runner().DoCompile(origin);
                Assert.IsFalse(string.IsNullOrEmpty(sw.ToString()));
            }
        }
    }
}
