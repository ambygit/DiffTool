using Hsbc.Task.Tools.DiffLibrary.Engine.Main;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;
using Hsbc.Task.Tools.TestUtil.Base;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenLinesRangeDiffCalculatorWithDifferencesOnly
{
    [TestFixture(@"GivenLinesRangeDiffCalculatorWithDifferencesOnly\FilesAndExpectations\Set1", "Source.txt", "Target.txt", "Expectation.txt")]
    [TestFixture(@"GivenLinesRangeDiffCalculatorWithDifferencesOnly\FilesAndExpectations\Set2", "Source.txt", "Target.txt", "Expectation.txt")]
    public abstract class GivenLinesRangeDiffCalculatorWithDifferencesOnlyBase : GivenWhenThen
    {        
        protected readonly string _sourceFile;
        protected readonly string _targetFile;
        protected readonly string _serializedResultExpectation;        
        protected DiffCalculator _diffCalculator;

        protected GivenLinesRangeDiffCalculatorWithDifferencesOnlyBase(string baseDirectory, string sourceFile, string targetFile, string serializedResultExpectation)
        {
            _sourceFile = GetFilePath(baseDirectory, sourceFile);
            _targetFile = GetFilePath(baseDirectory, targetFile);
            _serializedResultExpectation = GetFilePath(baseDirectory, serializedResultExpectation); ;
        }

        public override void Given()
        {            
            _diffCalculator = new DiffCalculator(new DefaultDiffResultOption());
        }

        private string GetFilePath(string baseDirectory, string fileName)
        {
            return string.Format(@"{0}\{1}", baseDirectory, fileName);
        }
    }
}