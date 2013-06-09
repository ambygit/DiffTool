using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.TestUtil.Base;

namespace Hsbc.Task.Tools.DiffLibraryTests.GivenLinesDiffResult
{
    public abstract class GivenLinesDiffResultBase : GivenWhenThen
    {
        protected LinesRangeDiffResult _linesDiffResult;

        public override void Given()
        {
            _linesDiffResult = new LinesRangeDiffResult();
            var affectedLines = new List<string>();
            affectedLines.Add("This is an important notice! It should");
            affectedLines.Add("therefore be located at the beginning of");
            affectedLines.Add("this document!");
            _linesDiffResult.AffectedLines = affectedLines;
            _linesDiffResult.SourceRange = new Range(0, 0);
            _linesDiffResult.TargetRange = new Range(1, 4);
            _linesDiffResult.DiffStatus = EDiffStatus.Added;
           ;
        }
    }
}