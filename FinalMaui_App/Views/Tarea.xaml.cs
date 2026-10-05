using FinalMaui_App.ViewModels;
namespace FinalMaui_App.Views;


public partial class Tarea : ContentPage
{
	public Tarea()
	{
		InitializeComponent();
		BindingContext = new TareaViewModel();
	}

    private void GoBackBtn_Clicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}