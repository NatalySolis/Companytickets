using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class detalle_ticket
    {
        [Key]
        public int iddetalle { get; set; }
        public int idticket { get; set; }
        public int idempleado { get; set; }
        public int idestado { get; set; }


    }
}
