using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using EvidencijaPorudzbina.UI.ViewModels.Windows;
using EvidencijaPorudzbina.PristupBaziPodataka.Repozitorijumi;

namespace EvidencijaPorudzbina.UI.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Register Services
            SimpleIoc.Default.Register<IRepozitorijumPorudzbina, RepozitorijumPorudzbina>();
            #endregion

            #region Register Views
            SimpleIoc.Default.Register<GlavniProzorViewModel>();
            SimpleIoc.Default.Register<DodavanjePorudzbinaViewModel>();
            #endregion
        }

        public GlavniProzorViewModel GlavniProzorView => ServiceLocator.Current.GetInstance<GlavniProzorViewModel>();
        public DodavanjePorudzbinaViewModel DodavanjeView => ServiceLocator.Current.GetInstance<DodavanjePorudzbinaViewModel>();

        public static void Cleanup() { }
    }
}
