using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenLinesDiffResult
{
    public class WhenGetResultIsCalled : GivenLinesDiffResultBase
    {
        private string _result;

        public override void When()
        {
            _result = _linesDiffResult.ToString();
        }

        [Test]
        public void ThenWeGetSetOfFormattedResult()
        {
            Assert.IsTrue(_result!=null);
        }
    }
}