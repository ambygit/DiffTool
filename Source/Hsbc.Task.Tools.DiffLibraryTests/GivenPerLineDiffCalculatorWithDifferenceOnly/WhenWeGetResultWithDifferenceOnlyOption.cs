using System.Collections.Generic;
using Hsbc.Task.Tools.CommonUtil.SerializationUtil;
using Hsbc.Task.Tools.DiffLibrary.Model;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenPerLineDiffCalculatorWithDifferenceOnly
{   
    public class WhenWeGetResultWithDifferenceOnlyOption : GivenPerLineDiffCalculatorWithDifferenceOnlyBase
    {
        private List<IDiffResult> _result;
        protected List<PerLineDiffResult> _resultExpectations;

        public WhenWeGetResultWithDifferenceOnlyOption(string baseDirectory, string sourceFile, string targetFile, string serializedResultExpectation)
            : base(baseDirectory, sourceFile, targetFile, serializedResultExpectation)
        {
        }

        public override void When()
        {         
            _result = _diffCalculator.Run(_sourceFile, _targetFile).GetRecords();
            _resultExpectations = XmlSerializationHelper.DeSerializeFromFile<List<PerLineDiffResult>>(_serializedResultExpectation);
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