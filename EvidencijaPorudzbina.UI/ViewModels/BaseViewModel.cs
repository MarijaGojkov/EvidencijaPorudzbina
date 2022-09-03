using CommunityToolkit.Mvvm.ComponentModel;
using EvidencijaPorudzbina.UI.Models;
using System.Windows;

namespace EvidencijaPorudzbina.UI.ViewModels
{
    public class BaseViewModel<TModel> : ObservableRecipient where TModel : BaseModel, new()
	{
		public BaseViewModel()
		{
			Model = new TModel();
		}

		public TModel Model { get; set; }

		public void Close()
		{
			foreach (Window item in Application.Current.Windows)
			{
				if (item.DataContext == this) item.Close();
			}
		}
	}
}
