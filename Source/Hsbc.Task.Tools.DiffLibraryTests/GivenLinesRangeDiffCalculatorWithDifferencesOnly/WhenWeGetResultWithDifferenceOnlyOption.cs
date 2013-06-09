using System.Collections.Generic;
using System.Linq;
using Hsbc.Task.Tools.CommonUtil.SerializationUtil;
using Hsbc.Task.Tools.DiffLibrary.Model;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenLinesRangeDiffCalculatorWithDifferencesOnly
{
    public class WhenWeGetResultWithDifferenceOnlyOption : GivenLinesRangeDiffCalculatorWithDifferencesOnlyBase
    {
        private List<IDiffResult> _result;
        protected List<LinesRangeDiffResult> _resultExpectations;

        public WhenWeGetResultWithDifferenceOnlyOption(string baseDirectory, string sourceFile, string targetFile, string serializedResultExpectation)
            : base(baseDirectory, sourceFile, targetFile, serializedResultExpectation)
        {
        }

        public override void When()
        {
            _resultExpectations = XmlSerializationHelper.DeSerializeFromFile<List<LinesRangeDiffResult>>(_serializedResultExpectation);
            _result = _diffCalculator.Run(_sourceFile, _targetFile).GetRecords();
            var serizlized = XmlSerializationHelper.Serialize(_result.Cast<LinesRangeDiffResult>().ToList());
        }

        [Test]
        public void ThenExpectationsAreMet()
        {
            //Iterate and match each result, expecting an ordered match
            var resultIndex = 0;
            foreach (var resultExpectation in _resultExpectations)
            {
                var respectiveResult = _result[resultIndex];
                Assert.IsTrue(resultExpectation.Equals(respectiveResult), string.Format("Expected:{0}, Got:{1}",resultExpectation,respectiveResult));

                resultIndex++;
            }

            Assert.Pass("All expectation are met");
        }
    }
}