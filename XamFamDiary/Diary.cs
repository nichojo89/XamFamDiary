using PropertyChanged;
using System.Collections.ObjectModel;

namespace XamFamDiary
{
    /// <summary>
    /// Daily diary object
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Diary
    {
        public string Tag { get; set; }
        public string Area { get; set; }
        public string Date { get; set; }
        public string Event { get; set; }
        public bool LinkEvent { get; set; }
        public string Comments { get; set; }
        public string Category { get; set; }
        public bool IncludePhoto { get; set; }
        public ObservableCollection<string> Photos { get; set; }
    }
}