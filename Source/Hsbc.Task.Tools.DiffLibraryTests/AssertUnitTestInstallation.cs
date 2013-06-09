using System;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests
{
    [TestFixture]
    public class AssertUnitTestInstallation
    {
        [Test]
        [ExpectedException]
        public void FailingTest()
        {
            throw new ApplicationException();
        }

        [Test]
        public void PassingTest()
        {
            Assert.Pass();
        }
    }
}