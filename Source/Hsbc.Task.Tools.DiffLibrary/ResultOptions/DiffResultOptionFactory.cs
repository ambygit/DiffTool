using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public class DiffResultOptionFactory
    {
        private static readonly DiffResultOptionFactory _instance;

        static DiffResultOptionFactory()
        {
            _instance = new DiffResultOptionFactory();
        }

        public static DiffResultOptionFactory Instance
        {
            get { return _instance; }
        }

        public IDiffResultOption<IDiffResult> Create(DiffOptions diffOptions)
        {
            if (diffOptions.ShouldShowAll)
            {
                if (diffOptions.ShouldShowPerLine)
                {
                    return new ShowAllInPerLineStyleResultOption();    
                }
                return new ShowAllInLineRangeStyleResultOption();
            }
            //else
            {
                if (diffOptions.ShouldShowPerLine)
                {
                    return new ShowOnlyDifferencesInPerLineStyleResultOption();    
                }                
            }

            //Default 
            return new DefaultDiffResultOption();
        }
    }
}