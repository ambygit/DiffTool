using System.Collections.Generic;
using System.Linq;
using Hsbc.Task.Tools.CommonUtil.SerializationUtil;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibraryTests.Helper;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenCommonUtil
{
    public class WhenXmlSerializationHelperIsUsed : GivenCommonUtilBase
    {
        private List<LinesRangeDiffResult> _linesRangeDiffResultSource;
        private List<PerLineDiffResult> _perLineDiffResultSource;

        public override void When()
        {
            _linesRangeDiffResultSource = SampleData.GetSampleLinesDiffResults();
            _perLineDiffResultSource = SampleData.GetSamplePerLineDiffResults();
        }

        [Test]
        public void ThenSerializaionAndDeserializationWorksForLinesRangeDiffResult()
        {
            var serialized = XmlSerializationHelper.Serialize(_linesRangeDiffResultSource);
            var deserialized = XmlSerializationHelper.DeSerialize<List<LinesRangeDiffResult>>(serialized);

            Assert.IsTrue(_linesRangeDiffResultSource.Count==deserialized.Count);
            Assert.IsTrue(_linesRangeDiffResultSource.First().Equals(deserialized.First()));

        }

        [Test]
        public void ThenSerializaionAndDeserializationWorksForPerLineDiffResult()
        {
            var serialized = XmlSerializationHelper.Serialize(_perLineDiffResultSource);
            var deserialized = XmlSerializationHelper.DeSerialize<List<PerLineDiffResult>>(serialized);

            Assert.IsTrue(_perLineDiffResultSource.Count == deserialized.Count);
            Assert.IsTrue(_perLineDiffResultSource.First().Equals(deserialized.First()));

        }
    }
}