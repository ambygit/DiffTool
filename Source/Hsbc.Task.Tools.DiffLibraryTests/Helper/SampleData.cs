using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibraryTests.Helper
{
    public class SampleData
    {
        public static List<LinesRangeDiffResult> GetSampleLinesDiffResults()
        {
            var expectations = new List<LinesRangeDiffResult>();

            var result = new LinesRangeDiffResult();
            var affectedLines = new List<string>();
            affectedLines.Add("");
            affectedLines.Add("But 2 added has more to say");
            result.AffectedLines = affectedLines;
            result.SourceRange = new Range(4, 0);
            result.TargetRange = new Range(5, 6);
            result.DiffStatus = EDiffStatus.Added;
            expectations.Add(result);           

            return expectations;
        }
        public static List<LinesRangeDiffResult> GetSampleLinesDiffResults2()
        {
            var expectations = new List<LinesRangeDiffResult>();

            var result = new LinesRangeDiffResult();
            var affectedLines = new List<string>();
            affectedLines.Add("This is an important notice! It should");
            affectedLines.Add("therefore be located at the beginning of");
            affectedLines.Add("this document!");
            result.AffectedLines = affectedLines;
            result.SourceRange = new Range(0, 0);
            result.TargetRange = new Range(1, 4);
            result.DiffStatus = EDiffStatus.Added;
            expectations.Add(result);

            result = new LinesRangeDiffResult();
            affectedLines = new List<string>();
            affectedLines.Add("This paragraph contains text that is");
            affectedLines.Add("outdated - it will be deprecated '''and'''");
            affectedLines.Add("deleted '''in''' the near future.");
            result.AffectedLines = affectedLines;
            result.SourceRange = new Range(4, 7);
            result.TargetRange = new Range(7, 0);
            result.DiffStatus = EDiffStatus.Added;
            expectations.Add(result);

            return expectations;
        }

        public static List<PerLineDiffResult> GetSamplePerLineDiffResults()
        {
            var expectations = new List<PerLineDiffResult>();

            var result = new PerLineDiffResult();
            var affectedLines = "This is an important notice! It should";
            result.AffectedLine = affectedLines;
            result.LinePosition= 1;
            result.DiffStatus = EDiffStatus.Added;
            expectations.Add(result);

            result = new PerLineDiffResult();
            affectedLines = "This paragraph contains text that is";
            result.AffectedLine = affectedLines;
            result.LinePosition = 7;
            result.DiffStatus = EDiffStatus.Added;
            expectations.Add(result);

            return expectations;
        }
    }

}