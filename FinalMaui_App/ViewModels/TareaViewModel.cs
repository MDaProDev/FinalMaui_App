using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalMaui_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinalMaui_App.Services;

namespace FinalMaui_App.ViewModels
{
    internal class TareaViewModel : ObservableObject
    {
        public ObservableCollection<Tarea> Tareas { get; set; }
        //Instancing Producto so that it aint null
        public Tarea Tarea { get; set; } = new Tarea();
        public TareaViewModel()
        {
            Refresh();

            Tareas = new ObservableCollection<Tarea>
            {
                new Tarea { Description ="Buy extra", Done=false}
            };

            Tarea = new Tarea();

            AddTareasCommand = new AsyncRelayCommand(AddTarea);
            RefreshTareaCommand = new AsyncRelayCommand(Refresh);
            DeleteTareaCommand = new AsyncRelayCommand<Tarea>(Delete);

        }
        //Commands
        public ICommand AddTareasCommand { get; }
        public ICommand DeleteTareaCommand { get; }
        public ICommand RefreshTareaCommand { get; }


        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        async Task AddTarea()
        {
            if (string.IsNullOrEmpty(Tarea.Description))
            {
                await AppShell.Current.DisplayAlert("Error", "Products require a name", "Ok");
                return;
            }
            

            //Este codigo debería obtener producto.Name etc mediante Entry en xaml usando Binding

            //He intentado obtener el precio y stock mediante DisplayPromptAsync
            //Pero este no acepta nada que no sea de tipo String

            try
            {
                //var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name", "OK", "Cancel");


                //await ProductoDbService.AddProducto(name, price, stock);

                //var name = await App.Current.Windows?.FirstOrDefault()?.Page.DisplayPromptAsync("Name", "Name", "OK", "Cancel");
                //float price = await App.Current.Windows?.FirstOrDefault()?.Page.DisplayPromptAsync("Price", "Price", "OK", "Cancel");
                //int stock = await App.Current.Windows?.FirstOrDefault()?.Page.DisplayPromptAsync("Stock", "Stock", "OK", "Cancel");


                await ProductoDbService.AddTarea(Tarea.Description, Tarea.Date, Tarea.Done, Tarea.ProducotId);
                await Refresh();


            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }




            //await ProductoDbService.AddProducto(producto.Name, producto.Price, producto.Stock);

        }

        async Task Delete(Tarea tarea)
        {

            try
            {
                await ProductoDbService.DeleteTarea(tarea.Id);
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            await Refresh();
        }

        async Task Refresh()
        {
            //Este Task debería cargar los productos en la lista

            if (isBusy) return;
            isBusy = true;
            try
            {
                var tareas = await ProductoDbService.GetTarea();
                Tareas.Clear();

                foreach (var tarea in tareas)
                {
                    Tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                isBusy = false;
            }


        }


    }
}
