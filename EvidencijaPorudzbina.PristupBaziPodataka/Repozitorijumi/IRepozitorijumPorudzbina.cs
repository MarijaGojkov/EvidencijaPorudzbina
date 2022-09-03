using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;

namespace EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi
{
    public interface IRepozitorijumPorudzbina
    {
        Modeli.Porudzbina UzmiPorudzbinuPoId(int id);
        List<Modeli.Porudzbina> GetAllPorudzbine();
        List<Modeli.Porudzbina> PretragaPorudzbina(string pretraga);
        void ObrisiPorudzbinuPoId(int id);
        int DodajPorudzbinu(Modeli.Porudzbina porudzbina);
        void IzmeniPorudzbinu(Modeli.Porudzbina porudzbina);
        void IzmeniStanjePorudzbine(int id, int idStanja);
        List<StanjePorudzbine> UzmiStanjaPorudzbine();
    }
}
