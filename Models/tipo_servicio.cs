using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class tipo_servicio
    {
        [Key]
        public int idtiposervicio { get; set; } 
        public string nombre { get; set; }
                
    }
}
