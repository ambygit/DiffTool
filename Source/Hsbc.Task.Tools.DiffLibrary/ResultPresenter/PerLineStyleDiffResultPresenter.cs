using System.Collections.Generic;
using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultPresenter
{
    public class PerLineStyleDiffResultPresenter : IDiffResultPresenter
    {
        public string PresentPrefix(EDiffStatus diffStatus)
        {
            return EDiffStatusDescription.GetPrefix(diffStatus);    
        }

        private static class EDiffStatusDescription
        {
            private static readonly Dictionary<EDiffStatus, string> EDiff2PrefixIdentifierMap;

            static EDiffStatusDescription()
            {
                EDiff2PrefixIdentifierMap = new Dictionary<EDiffStatus, string>();
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.None, "");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Added, "++");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Deleted, "--");
                EDiff2PrefixIdentifierMap.Add(EDiffStatus.Changed, "~~");

            }

            public static string GetPrefix(EDiffStatus diffStatus)
            {
                return EDiff2PrefixIdentifierMap[diffStatus];
            }
        }
    }
}