
using System;

namespace Hsbc.Task.Tools.DiffLibrary.Model
{
    public struct Range : IEquatable<Range>
    {
        private int _end;
        private int _start;

        public Range(int start, int end)
        {
            _start = start;
            _end = end;
        }

        public int Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public int End
        {
            get { return _end; }
            set { _end = value; }
        }

        public bool Equals(Range other)
        {
            return Start == other.Start && End == other.End;
        }

        public override string ToString()
        {
            return (_end == 0 || _start == _end) ? _start.ToString() : string.Format("{0},{1}", _start, _end);
        }
    }
}