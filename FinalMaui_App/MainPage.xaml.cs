using FinalMaui_App.Views;
using System.Threading.Tasks;

namespace FinalMaui_App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void GoToProductsBtn_Clicked(object? sender, EventArgs e)
        {
            Producto producto = new Producto();
            Navigation.PushAsync(producto);
        }

        private void GoToTasksBtn_Clicked(object sender, EventArgs e)
        {
            Tarea tarea = new Tarea();
            Navigation.PushAsync(tarea);
        }
    }
}
