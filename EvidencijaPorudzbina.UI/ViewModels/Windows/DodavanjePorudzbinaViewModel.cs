using EvidencijaPorudzbina.PristupBaziPodataka.Modeli;
using EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi;
using EvidencijaPorudzbina.UI.Models;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Windows;
using System;
using CommunityToolkit.Mvvm.Input;

namespace EvidencijaPorudzbina.UI.ViewModels.Windows
{
	public class DodavanjePorudzbinaViewModel : BaseViewModel<DodavanjePorudzbinaModel>
	{
		private readonly IRepozitorijumPorudzbina _repozitorijum;

		[PreferredConstructor]
		public DodavanjePorudzbinaViewModel(IRepozitorijumPorudzbina repozitorijum)
		{
			Model.NazivProzora = "Dodavanje Porudzbine";
			Model.NazivDugmeta = "Dodaj";
			_repozitorijum = repozitorijum;

			DodavanjeIzmenaKomanda = new RelayCommand(Potvrdi);
		}

		public DodavanjePorudzbinaViewModel(IRepozitorijumPorudzbina repozitorijum, Porudzbina porudzbina)
		{
			Model.NazivProzora = "Izmena Porudzbine";
			Model.NazivDugmeta = "Izmeni";
			_repozitorijum = repozitorijum;
			Porudzbina = porudzbina;

			DodavanjeIzmenaKomanda = new RelayCommand(Potvrdi);
			Model.Dostavljac = porudzbina.Dostavljac;
		}


		#region[Properties]
		public Porudzbina Porudzbina { get; set; }
		public RelayCommand DodavanjeIzmenaKomanda { get; set; }
		#endregion

		#region[Komande]
		private void IzmeniEkskurziju()
		{
			_repozitorijum.IzmeniPorudzbinu(new Porudzbina
			{
				Cena = Model.Cena,
				AdresaKupca = Model.AdresaKupca,
				Dostavljac = Model.Dostavljac,
				Proizvod = Model.Proizvod,
				TelefonKupca = Model.TelefonKupca
			});
			MessageBox.Show("Uspesno ste izmenili ekskurziju!", "Uspeh");
		}

		private void DodajNovuEkskurziju()
		{
			_repozitorijum.DodajPorudzbinu(new Porudzbina
			{
				Cena = Model.Cena,
				DatumPorucivanja = DateTime.UtcNow,
				AdresaKupca = Model.AdresaKupca,
				Dostavljac = Model.Dostavljac,
				Proizvod = Model.Proizvod,
				TelefonKupca = Model.TelefonKupca
			});
			MessageBox.Show("Uspesno ste dodali novu ekskurziju!", "Uspeh");
		}

		private void Potvrdi()
		{
			if (Porudzbina == null)
			{
				DodajNovuEkskurziju();
			}
			else if (Porudzbina != null)
			{
				IzmeniEkskurziju();
			}

			Close();
		}
		#endregion
	}
}
