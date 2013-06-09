using Hsbc.Task.Tools.DiffLibrary.Engine.Main;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;
using Hsbc.Task.Tools.TestUtil.Base;
using NUnit.Framework;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenPerLineDiffCalculatorWithDifferenceOnly
{
    [TestFixture(@"GivenPerLineDiffCalculatorWithDifferenceOnly\FilesAndExpectations\Set1", "Source.txt", "Target.txt", "Expectation.txt")]
    [TestFixture(@"GivenPerLineDiffCalculatorWithDifferenceOnly\FilesAndExpectations\Set2", "Source.txt", "Target.txt", "Expectation.txt")]
    [TestFixture(@"GivenPerLineDiffCalculatorWithDifferenceOnly\FilesAndExpectations\Set3", "Source.txt", "Target.txt", "Expectation.txt")]
    public abstract class GivenPerLineDiffCalculatorWithDifferenceOnlyBase : GivenWhenThen
    {        
        protected readonly string _sourceFile;
        protected readonly string _targetFile;
        protected readonly string _serializedResultExpectation;        
        protected DiffCalculator _diffCalculator;

        protected GivenPerLineDiffCalculatorWithDifferenceOnlyBase(string baseDirectory, string sourceFile, string targetFile, string serializedResultExpectation)
        {
            _sourceFile = GetFilePath(baseDirectory, sourceFile);
            _targetFile = GetFilePath(baseDirectory, targetFile);
            _serializedResultExpectation = GetFilePath(baseDirectory, serializedResultExpectation); ;
        }

        public override void Given()
        {            
            _diffCalculator = new DiffCalculator(new ShowOnlyDifferencesInPerLineStyleResultOption());
        }

        private string GetFilePath(string baseDirectory, string fileName)
        {
            return string.Format(@"{0}\{1}", baseDirectory, fileName);
        }
    }
}