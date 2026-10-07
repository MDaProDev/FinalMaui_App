using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalMaui_App.Models;
using FinalMaui_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalMaui_App.ViewModels
{
    internal class ProductoViewModel : ObservableObject
    {
        
        public ObservableCollection<Producto> Productos { get; set; }
        //Instancing Producto so that it aint null
        public Producto Producto { get; set; } = new Producto();
        public ProductoViewModel() 
        { 
            Refresh();

            Productos = new ObservableCollection<Producto>
            {
                new Producto {Name="Hat", Price=30, Stock=60}
            };

            Producto = new Producto();
            
            AddProductosCommand = new AsyncRelayCommand(AddProducto);
            RefreshProductosCommand = new AsyncRelayCommand(Refresh);
            DeleteProductoCommand = new AsyncRelayCommand<Producto>(Delete);

        }
        //Commands
        public ICommand AddProductosCommand { get; }
        public ICommand DeleteProductoCommand { get; }
        public ICommand RefreshProductosCommand { get; }


        bool isBusy;
        public bool IsBusy
        {
            get => isBusy; 
            set => SetProperty(ref isBusy, value);
        }

        async Task AddProducto()
        {
            if (string.IsNullOrEmpty(Producto.Name))
            {
                await AppShell.Current.DisplayAlert("Error", "Products require a name","Ok");
                return;
            }
            if (Producto.Price < 0.1)
            {
                await AppShell.Current.DisplayAlert("Error", "Products cant have negative price or none at all", "Ok");
                return;
            }

            try
            {

                await ProductoDbService.AddProducto(Producto.Name, Producto.Price, Producto.Stock);
                await Refresh();
                

            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }


            

            //await ProductoDbService.AddProducto(producto.Name, producto.Price, producto.Stock);
            
        }

        async Task Delete(Producto producto)
        {
            
            try
            {
                await ProductoDbService.DeleteProducto(producto.Id);
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
                var productos = await ProductoDbService.GetProducto();
                Productos.Clear();
        
                foreach (var producto in productos)
                {
                    Productos.Add(producto);
                }
            }
            catch(Exception ex)
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
