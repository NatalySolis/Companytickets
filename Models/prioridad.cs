using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class prioridad
    {
        [Key]
        public int idpriori { get; set; }
        public string nombre { get; set; }
            
    }
}
