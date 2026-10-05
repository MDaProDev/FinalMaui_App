using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalMaui_App.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }


        [Indexed]
        public int ProducotId { get; set; } 


    }
}
