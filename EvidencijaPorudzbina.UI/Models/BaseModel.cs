using System.ComponentModel;

namespace EvidencijaPorudzbina.UI.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string NazivProzora { get; set; }
    }
}
