using EvidencijaPorudzbina.UI.Models;
using EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi;
using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;

namespace EvidencijaPorudzbina.UI.ViewModels.Windows
{
    public class GlavniProzorViewModel : BaseViewModel<GlavniProzorModel>
    {
        private readonly IRepozitorijumPorudzbina _repozitorijum;
        public GlavniProzorViewModel(IRepozitorijumPorudzbina repozitorijum, IRepozitorijumPorudzbina repozitorijumPorudzbina)
        {
            Model.NazivProzora = "Evidencija porudzbina";
            Model.Porudzbine = new ObservableCollectiona<Porudzbina>(repozitorijumPorudzbina.GetAllPorudzbine());
            Model.PorudzbineView = CollectionViewSource.GetDefaultView(Model.Porudzbine) as ListCollectionView;

            _repozitorijum = repozitorijum;

            PretragaKomanda = new RelayCommand(PretragaPorudzbina);
            DodavanjePorudzbineKomanda = new RelayCommand(DodavanjePorudzbine);
            IzmeniPorudzbinuKomanda = new RelayCommand(IzmenaPorudzbine);
            ObrisiPorudzbinuKomanda = new RelayCommand(BrisanjePorudzbine);
            EksportXMLKomandaa = new RelayCommand(EksportXML);
            PrikaziSvePorudzbineKomanda = new RelayCommand(PrikaziSvePorudzbine);
        }
    }

    public RelayCommand PretragaKomanda { get; set; }
    public RelayCommand DodavanjePorudzbineKomanda { get; set; }
    public RelayCommand IzmeniPorudzbinuKomanda { get; set; }
    public RelayCommand ObrisiPorudzbinuKomanda { get; set; }
    public RelayCommand EksportXMLKomanda { get; set; }
    public RelayCommand PrikaziSvePorudzbineKomanda { get; set; }

    public void PrikaziSvePorudzbine()
    {
        Model.Porudzbine = new ObservableCollection<Porudzbina>(PristupBaziPodataka.Repozitorijumi.RepozitorijumPorudzbina.GetAllPorudzbine());

        Model.GlavniProzorView = CollectionViewSource.GetDefaultView(Model.Porudzbine) as ListCollectionView;

        Model.Pretraga = string.Empty;
    }

    public void IzmenaPorudzbine()
    {
        if (Model.IzabranaPorudzbina != null)
        {
            FormaDodavanje forma = new FormaDodavanje
            {
                DataContext = new DodavanjeWindowViewModel(Model.IzabranaPorudzbina)
            };
            forma.Show();
        }
    }



    private void PretragaPorudzbina()
    {
        if (Model.Pretraga == string.Empty)
        {
            PrikaziSvePorudzbine();
        }
        else
        {
            Model.Porudzbine = new ObservableCollection<Porudzbina>(PristupBaziPodataka.Repozitorijumi.RepozitorijumPorudzbina(Model.Pretraga));

            Model.EkskurzijeView = CollectionViewSource.GetDefaultView(Model.Ekskurzije) as ListCollectionView;
        }
    }


    internal class ObservableCollectiona<T> : ObservableCollection<Porudzbina>
    {
        public ObservableCollectiona(IEnumerable<Porudzbina> collection) : base(collection)
        {
        }
    }
}
