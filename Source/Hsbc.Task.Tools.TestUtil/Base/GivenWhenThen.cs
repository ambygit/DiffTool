using NUnit.Framework;

namespace Hsbc.Task.Tools.TestUtil.Base
{
    [TestFixture]
    public abstract class GivenWhenThen
    {
        [SetUp]
        public void InitialSetup()
        {
            Given();
            When();
        }

        public abstract void Given();
        public abstract void When();
    }
}