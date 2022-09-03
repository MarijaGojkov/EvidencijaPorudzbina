using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace EvidencijaPorudzbina.UI.Models
{
    public class GlavniProzorModel : BaseModel
    {
        public int Id { get; set; }
        public string Dostavljac { get; set; }
        public string Proizvod { get; set; }
        public double Cena { get; set; }
        public string AdresaKupca { get; set; }
        public string TelefonKupca { get; set; }
        public DateTime DatumPorucivanja { get; set; }

        public Porudzbina IzabranaPorudzbina { get; set; } 
        public ObservableCollection<Porudzbina> Porudzbine { get; set; }

        public string NazivProzora { get; set; }
        public string Pretraga { get; set; }

        public ListCollectionView PorudzbineView { get; set; }


    }
}
