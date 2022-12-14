namespace EvidencijaPorudzbina.PristupBaziPodataka.Modeli
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public string Dostavljac { get; set; }
        public string Proizvod { get; set; }
        public decimal Cena { get; set; }
        public string AdresaKupca  { get; set; }
        public string TelefonKupca { get; set; }
        public string StanjePorudzbine { get; set; }
		public DateTime DatumPorucivanja { get; set; }
    }
}
