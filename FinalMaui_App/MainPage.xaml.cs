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

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
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
