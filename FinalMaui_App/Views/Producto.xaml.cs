using FinalMaui_App.ViewModels;
using System.Threading.Tasks;

namespace FinalMaui_App.Views;

public partial class Producto : ContentPage
{
	public Producto()
	{
		
		InitializeComponent();
		BindingContext = new ProductoViewModel();
	}

    private void GoBackBtn_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }
}