using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class rol
    {
        [Key]
        public int idrol { get; set; }
        public string nombrerol {  get; set; }

    }
}
