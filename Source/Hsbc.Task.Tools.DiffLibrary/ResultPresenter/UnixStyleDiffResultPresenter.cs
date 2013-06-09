using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultPresenter
{
    public class UnixStyleDiffResultPresenter : IRangeDiffResultPresenter
    {
        public string PresentRange(Range sourceRange, EDiffStatus diffStatus, Range targetRange)
        {
            return string.Format("{0}{1}{2}", sourceRange,
                                 EDiffStatusDescription.GetDescription(diffStatus),
                                 targetRange);
        }

        public string PresentPrefix(EDiffStatus diffStatus)
        {
            return EDiffStatusDescription.GetPrefix(diffStatus);
        }

        private static class EDiffStatusDescription
        {
            private static readonly Dictionary<EDiffStatus, char> EDiff2DescriptionMap;
            private static readonly Dictionary<EDiffStatus, string> EDiff2PrefixIdentifierMap;

            static EDiffStatusDescription()
            {
                EDiff2DescriptionMap = new Dictionary<EDiffStatus, char>();
                EDiff2DescriptionMap.Add(EDiffStatus.None, '?');
                EDiff2DescriptionMap.Add(EDiffStatus.Added, 'a');
                EDiff2DescriptionMap.Add(EDiffStatus.Deleted, 'd');
                EDiff2DescriptionMap.Add(EDiffStatus.Changed, 'c');

                EDiff2PrefixIdentifierMap = new Dictionary<EDiffStatus, string>();
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.None, "");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Added, ">");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Deleted, "<");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Changed, "~");

            }

            public static char GetDescription(EDiffStatus diffStatus)
            {
                return EDiff2DescriptionMap[diffStatus];
            }

            public static string GetPrefix(EDiffStatus diffStatus)
            {
                return EDiff2PrefixIdentifierMap[diffStatus];
            }
        }
    }
}