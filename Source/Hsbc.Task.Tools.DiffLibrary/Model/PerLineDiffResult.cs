using System;

namespace Hsbc.Task.Tools.DiffLibrary.Model
{
    public class PerLineDiffResult : IDiffResult, IEquatable<PerLineDiffResult>
    {
        public static PerLineDiffResult Create(EDiffStatus diffStatus, int recordPosition, string recordSource, bool isSourceRecord)
        {
            return new PerLineDiffResult()
                       {AffectedLine = recordSource, DiffStatus = diffStatus, LinePosition = recordPosition, IsSourceRecord = isSourceRecord};
        }

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as PerLineDiffResult);
        }

        public bool Equals(PerLineDiffResult other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            //Trying to short circuit with simple comparisons first.
            return LinePosition==other.LinePosition &&
                   DiffStatus == other.DiffStatus && AffectedLine.Equals(other.AffectedLine);
        }

        public override int GetHashCode()
        {
            //For Hashcode, relying on implementation of string to get a distributed hash
            return
                string.Format("AffectedLine: {0}, LinePosition: {1}, DiffStatus: {2}", AffectedLine, LinePosition,
                              DiffStatus).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("AffectedLine: {0}, LinePosition: {1}, DiffStatus: {2}", AffectedLine, LinePosition,
                              DiffStatus);
        }

        public string AffectedLine { get; set; }
        public int LinePosition { get; set; }
        public EDiffStatus DiffStatus { get; set; }
        //Required in case of Change, it won;t be possible to tell otherwise
        public bool IsSourceRecord { get; set; }
    }
}