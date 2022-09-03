using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
using System;
using System.Collections.Generic;
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
        public string StanjePorudzbine { get; set; }
        public DateTime DatumPorucivanja { get; set; }
		public ListCollectionView PorudzbineView { get; set; }

		public Porudzbina IzabranaPorudzbina { get; set; }
		public int IdStanja { get; set; }
		public ObservableCollection<Porudzbina> Porudzbine { get; set; }
        public ObservableCollection<StanjePorudzbine> StanjaPoruzdbine { get; set; }
        public string Pretraga { get; set; }
    }
}
