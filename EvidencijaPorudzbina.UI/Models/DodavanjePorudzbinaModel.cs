using System.Collections.ObjectModel;
using System.Windows.Data;
using System;
using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;

namespace EvidencijaPorudzbina.UI.Models
{
	public class DodavanjePorudzbinaModel : BaseModel
	{
		public int IdPorudzbine { get; set; }
		public string Dostavljac { get; set; }
		public string Proizvod { get; set; }
		public decimal Cena { get; set; }
		public string AdresaKupca { get; set; }
		public string TelefonKupca { get; set; }
		public DateTime DatumPorucivanja { get; set; }
		public DateTime DatumPorudzbina { get; set; }
		public string NazivDugmeta { get; set; }
	}
}
