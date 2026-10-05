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
                await AppShell.Current.DisplayAlert("Error", "Tasks require a description", "Ok");
                return;
            }

            try
            {
 
                await ProductoDbService.AddTarea(Tarea.Description, Tarea.Date, Tarea.Done, Tarea.ProducotId);
                await Refresh();


            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
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
