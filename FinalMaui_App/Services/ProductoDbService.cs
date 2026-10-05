using FinalMaui_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMaui_App.Services
{
    public static class ProductoDbService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Prods.db");
            db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Producto>();

            await db.CreateTableAsync<Tarea>();
        }

        //Producto
        public static async Task AddProducto(string name, float price, int stock)
        {
            await Init();
            var product = new Producto
            {
                Name = name,
                Price = price,
                Stock = stock
            };
            await db.InsertAsync(product);

        }
        public static async Task DeleteProducto(int id)
        {
            await Init();
            await db.DeleteAsync<Producto>(id);
        }
        public static async Task<IEnumerable<Producto>> GetProducto()
        {
            await Init();
            var product = await db.Table<Producto>().ToListAsync();
            return product;
        }

        //Tarea

        public static async Task AddTarea(string description, DateTime date, bool done, int productoId)
        {
            await Init();
            var tarea = new Tarea
            {
                Description = description,
                Date = date,
                Done = done,
                ProducotId = productoId
            };

            await db.InsertAsync(tarea);

        }
        public static async Task UpdateTarea(string description, DateTime date, bool done, int productoId)
        {
            await Init();
            var tarea = new Tarea
            {
                Description = description,
                Date = date,
                Done = done,
                ProducotId = productoId
            };
            await db.UpdateAsync(tarea);
        }

        public static async Task DeleteTarea(int id)
        {
            await Init();

            await db.DeleteAsync<Tarea>(id);
        }
        public static async Task<IEnumerable<Tarea>> GetTarea()
        {
            await Init();
            var tarea = await db.Table<Tarea>().ToListAsync();
            return tarea;
        }
    }
}
