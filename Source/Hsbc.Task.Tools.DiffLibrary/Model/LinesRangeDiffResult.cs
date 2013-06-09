using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hsbc.Task.Tools.DiffLibrary.Model
{
    public class LinesRangeDiffResult : IDiffResult, IEquatable<LinesRangeDiffResult>
    {
        public LinesRangeDiffResult()
        {
            AffectedLines = new List<string>();
        }

        public static LinesRangeDiffResult CreateNew(PerLineDiffResult perLineDiffResult)
        {
            var affectedLines = new List<string> {perLineDiffResult.AffectedLine};
            var rangeStart = new Range(perLineDiffResult.LinePosition, 0);
            var defaultRange = new Range(0, 0);
            Range sourceRange = defaultRange;
            Range targetRange= defaultRange;
            if(perLineDiffResult.IsSourceRecord)
            {
                sourceRange = rangeStart;
            }
            else
            {
                targetRange = rangeStart;
                if(perLineDiffResult.DiffStatus==EDiffStatus.Added)
                {
                    //In case of added, source range starts from one line prior to target start.
                    sourceRange = new Range(targetRange.Start - 1, 0);
                }
            }
            return new LinesRangeDiffResult
                       {
                           AffectedLines = affectedLines,
                           DiffStatus = perLineDiffResult.DiffStatus,
                           SourceRange = sourceRange,
                           TargetRange = targetRange
                       };
        }

        public void Modify(PerLineDiffResult perLineDiffResult)
        {
            Debug.Assert(DiffStatus==perLineDiffResult.DiffStatus,"Modification of Diff status is not allowed.");
            AffectedLines.Add(perLineDiffResult.AffectedLine);
            //With modification, it's going to effect the end range
            if(perLineDiffResult.IsSourceRecord)
            {
                SourceRange = new Range(SourceRange.Start, perLineDiffResult.LinePosition);
            }
            else
            {
                TargetRange = new Range(TargetRange.Start, perLineDiffResult.LinePosition);
            }
        }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as LinesRangeDiffResult);
        }

        public bool Equals(LinesRangeDiffResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            //Trying to short circuit with simple comparisons first.
            return SourceRange.Equals(other.SourceRange) && TargetRange.Equals(other.TargetRange) &&
                   DiffStatus == other.DiffStatus && AffectedLines.Count == other.AffectedLines.Count &&
                   AffectedLines.SequenceEqual(other.AffectedLines);
        }

        public override int GetHashCode()
        {
            //For Hashcode, relying on implementation of string to get a distributed hash
            return string.Format("AffectedLines: {0}, SourceRange: {1}, TargetRange: {2}, DiffStatus: {3}",
                     string.Join(",", AffectedLines.ToArray()), SourceRange, TargetRange, DiffStatus).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("SourceRange: {0}, TargetRange: {1}, DiffStatus: {2}", SourceRange, TargetRange, DiffStatus);
        }

        public List<string> AffectedLines { get; set; } 
        public Range SourceRange { get; set; }
        public Range TargetRange { get; set; }
        public EDiffStatus DiffStatus { get; set; }
    }
}