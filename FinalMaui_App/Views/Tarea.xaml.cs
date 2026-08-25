using FinalMaui_App.ViewModels;
namespace FinalMaui_App.Views;


public partial class Tarea : ContentPage
{
	public Tarea()
	{
		InitializeComponent();
		BindingContext = new TareaViewModel();
	}
}