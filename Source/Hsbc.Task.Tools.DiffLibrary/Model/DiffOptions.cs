namespace Hsbc.Task.Tools.DiffLibrary.Model
{
    public class DiffOptions
    {
        private readonly bool _isDefault;
        public DiffOptions()
        {
            //Setting all to default values
            ShouldShowAll = false;
            ShouldShowPerLine = false;          
            _isDefault = true;
        }
        
        public bool ShouldShowAll { get; set; }
        public bool IsDefault { get { return _isDefault; } }
        public bool ShouldShowPerLine { get; set; }
        public string SourceFile { get; set; }
        public string TargetFile { get; set; }

        public override string ToString()
        {
            return string.Format("ShouldShowAll: {0}, ShouldShowPerLine: {1}", ShouldShowAll, ShouldShowPerLine);
        }
    }
}