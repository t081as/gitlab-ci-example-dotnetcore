#region MIT License
// The MIT License (MIT)
//
// Copyright © 2018 Tobias Koch <t.koch@tk-software.de>
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

#region Namespaces
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace MyProject.Test
{
    /// <summary>
    /// Contains unit tests for the <see cref="Greeter"/> class
    /// </summary>
    [TestClass]
    public class GreeterTest
    {
        #region Methods

        /// <summary>
        /// Checks the default behavior of the <see cref="Greeter.Greet(string)"/> method.
        /// </summary>
        [TestMethod]
        public void TestGreet()
        {
            const string testName = "Tobi";
            Greeter greeter = new Greeter();

            StringAssert.Contains(greeter.Greet(testName), testName);
        }

        /// <summary>
        /// Checks the behavior of the <see cref="Greeter.Greet(string)"/> method with an invalid argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGreetNull()
        {
            Greeter greeter = new Greeter();
            greeter.Greet(null);
        }

        /// <summary>
        /// Checks the behavior of the <see cref="Greeter.Greet(string)"/> method with an invalid argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGreetEmpty()
        {
            Greeter greeter = new Greeter();
            greeter.Greet(string.Empty);
        }

        #endregion
    }
}
