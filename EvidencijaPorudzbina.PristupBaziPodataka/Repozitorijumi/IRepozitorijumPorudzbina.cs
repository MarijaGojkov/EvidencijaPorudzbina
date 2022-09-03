using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi
{
    public interface IRepozitorijumPorudzbina
    {
        Modeli.Porudzbina UzmiPorudzbinuPoId(int id);
        List<Modeli.Porudzbina> GetAllPorudzbine();
        List<Modeli.Porudzbina> PretragaPorudzbina(string pretraga);
        Modeli.Porudzbina Delete(int id);
        int Add(Modeli.Porudzbina porudzbina);
        int Update(Modeli.Porudzbina porudzbina);
    }
}
