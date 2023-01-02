using BioCompiler;
using ToolSupporter.BioExceptions;

namespace TestCompiler.SignleOperationTest
{
    [TestClass]
    public class DropletMergerTest
    {
        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Function                                   //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// d1+d2 -> d3
        /// </summary>
        [TestMethod]
        public void FunctionTest1()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d3\",\r\n    \"line\": 3\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 4,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 15,\r\n    \"yValue\": 15,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 5,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMerger, Executor\",\r\n    \"line\": 6,\r\n    \"outDropletName\": \"d3\",\r\n    \"inDroplet1Name\": \"d1\",\r\n    \"inDroplet2Name\": \"d2\",\r\n    \"xDest\": 5,\r\n    \"yDest\": 9\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        /// <summary>
        /// d1+d2->d2
        /// </summary>
        [TestMethod]
        public void FunctionTest2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d2,d1,d2,5,9);\r\n";
            string expect = "[\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d1\",\r\n    \"line\": 1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletDeclarator, Executor\",\r\n    \"name\": \"d2\",\r\n    \"line\": 2\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 3,\r\n    \"name\": \"d1\",\r\n    \"xValue\": 15,\r\n    \"yValue\": 15,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletInputer, Executor\",\r\n    \"line\": 4,\r\n    \"name\": \"d2\",\r\n    \"xValue\": 4,\r\n    \"yValue\": 4,\r\n    \"size\": 0.1\r\n  },\r\n  {\r\n    \"$type\": \"Executor.Model.Operation.DropletMerger, Executor\",\r\n    \"line\": 5,\r\n    \"outDropletName\": \"d2\",\r\n    \"inDroplet1Name\": \"d1\",\r\n    \"inDroplet2Name\": \"d2\",\r\n    \"xDest\": 5,\r\n    \"yDest\": 9\r\n  }\r\n]";
            string result = new Runner().DoCompile(origin);
            Assert.AreEqual(expect, result);
        }

        //===========================================================================================//
        //                                                                                           //
        //                                    Test for Robustness                                    //
        //                                                                                           //
        //===========================================================================================//

        /// <summary>
        /// The first argument is not a declared droplet
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void IncorrectSyntaxTest()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d5,d1,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }


        /// <summary>
        /// The 2nd argument is not a declared droplet
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void IncorrectSyntaxTest2()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d888,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// The 3rd argument is not a declared droplet
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void IncorrectSyntaxTest3()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d5,d1,d88,5,9);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// The 4rth argument is not int
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectSyntaxException))]
        public void IncorrectSyntaxTest4()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d1,d2,5.0,9);\r\n";
            new Runner().DoCompile(origin);
        }


        /// <summary>
        /// The 4rth argument is not int
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectSyntaxException))]
        public void IncorrectSyntaxTest5()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d1,d2,sadasqwe,9);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D3 is not declared
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void D3IsNotDeclared()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }

        /// <summary>
        /// D2 is not declared
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void D2IsNotDeclared()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }


        /// <summary>
        /// D1 is not declared
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DropletNotDeclaredException))]
        public void D1IsNotDeclared()
        {
            string origin =
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }


        /// <summary>
        /// D3 has not been released
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(VariableNotReleasedException))]
        public void D3HasNotBeenReleased()
        {
            string origin =
                "droplet d1;\r\n" +
                "droplet d2;\r\n" +
                "droplet d3;\r\n" +
                "input(d1,15,15,0.1);\r\n" +
                "input(d2,4,4,0.1);\r\n" +
                "input(d3,10,10,0.1);\r\n" +
                "merge(d3,d1,d2,5,9);\r\n";
            new Runner().DoCompile(origin);
        }


    }
}
