using CommunityToolkit.Mvvm.Input;
using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
using EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi;
using EvidencijaPorudzbina.UI.Models;
using EvidencijaPorudzbina.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;

namespace EvidencijaPorudzbina.UI.ViewModels.Windows
{
	public class GlavniProzorViewModel : BaseViewModel<GlavniProzorModel>
    {
        private readonly IRepozitorijumPorudzbina _repozitorijum;

		public GlavniProzorViewModel(IRepozitorijumPorudzbina repozitorijum)
        {
            _repozitorijum = repozitorijum;
            Model.NazivProzora = "Evidencija porudzbina";
			Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.GetAllPorudzbine());
            Model.StanjaPoruzdbine = new ObservableCollection<StanjePorudzbine>(_repozitorijum.UzmiStanjaPorudzbine());

            Model.PorudzbineView = CollectionViewSource.GetDefaultView(Model.Porudzbine) as ListCollectionView;


            PretragaKomanda = new RelayCommand(PretragaPorudzbina);
            DodavanjePorudzbineKomanda = new RelayCommand(DodavanjePorudzbine);
            IzmeniPorudzbinuKomanda = new RelayCommand(IzmenaPorudzbine);
            ObrisiPorudzbinuKomanda = new RelayCommand(BrisanjePorudzbine);
            EksportXMLKomanda = new RelayCommand(EksportUXML);
            PrikaziSvePorudzbineKomanda = new RelayCommand(PrikaziSvePorudzbine);
			IzmeniStanjePorudzbineKomanda = new RelayCommand(IzmeniStanjePorudzbine);
        }

		public RelayCommand PretragaKomanda { get; set; }
		public RelayCommand DodavanjePorudzbineKomanda { get; set; }
		public RelayCommand IzmeniPorudzbinuKomanda { get; set; }
		public RelayCommand ObrisiPorudzbinuKomanda { get; set; }
		public RelayCommand EksportXMLKomanda { get; set; }
		public RelayCommand PrikaziSvePorudzbineKomanda { get; set; }
		public RelayCommand IzmeniStanjePorudzbineKomanda { get; set; }

		public void PrikaziSvePorudzbine()
		{
			Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.GetAllPorudzbine());

			Model.PorudzbineView = CollectionViewSource.GetDefaultView(Model.Porudzbine) as ListCollectionView;

			Model.Pretraga = string.Empty;
		}

		public void DodavanjePorudzbine()
		{
			DodavanjePorudzbinaView forma = new DodavanjePorudzbinaView
			{
				DataContext = new DodavanjePorudzbinaViewModel(_repozitorijum)
			};
			forma.Closing += (s, o) =>
			{
				Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.GetAllPorudzbine());
			};
			forma.Show();
		}

		public void BrisanjePorudzbine()
		{
			if (Model.IzabranaPorudzbina is null)
			{
				MessageBox.Show("Morate izabrati porudzbinu", "Greska");
			}
			else
			{
				_repozitorijum.ObrisiPorudzbinuPoId(Model.IzabranaPorudzbina.Id);
			}
		}

		public void IzmeniStanjePorudzbine()
		{
			if (Model.IzabranaPorudzbina is null || Model.IdStanja == 0)
			{
				MessageBox.Show("Morate izabrati porudzbinu i njeno novo stanje", "Greska");
			}
			else
			{
				_repozitorijum.IzmeniStanjePorudzbine(Model.IzabranaPorudzbina.Id, Model.IdStanja);

			}
			if (Model.Pretraga != null)
			{
				Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.PretragaPorudzbina(Model.Pretraga));
			}
			else
			{
				Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.GetAllPorudzbine());
			}
		}

		public void IzmenaPorudzbine()
		{
			if (Model.IzabranaPorudzbina != null)
			{
				DodavanjePorudzbinaView forma = new DodavanjePorudzbinaView
				{
					DataContext = new DodavanjePorudzbinaViewModel(_repozitorijum, Model.IzabranaPorudzbina)
				};
				forma.Closing += (s, o) =>
				{
					Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.GetAllPorudzbine());
				};
				forma.Show();
			}
			else
			{
				MessageBox.Show("Morate izabrati porudzbinu", "Greska");
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
				Model.Porudzbine = new ObservableCollection<Porudzbina>(_repozitorijum.PretragaPorudzbina(Model.Pretraga));

				Model.PorudzbineView = CollectionViewSource.GetDefaultView(Model.Porudzbine) as ListCollectionView;
			}
		}

		private void EksportUXML()
		{
			XmlSerializer xml = new XmlSerializer(typeof(Porudzbina));
			string path = @"Ekskurzija.xml";

			if (Model.IzabranaPorudzbina != null)
			{
				using (TextWriter streamWriter = new StreamWriter(path))
				{
					xml.Serialize(streamWriter, Model.IzabranaPorudzbina);
				}

				MessageBox.Show("Ekskurzija uspesno eksportovana kao XML", "Uspeh");
			}
			else
			{
				MessageBox.Show("Izaberite ekskurzjiu", "Greska");
			}
		}
	}
}
