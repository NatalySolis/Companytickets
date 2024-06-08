using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class ticket
    {
        [Key]
        public int idticket { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public int? idcuenta { get; set; }
        public int? idpriori { get; set; }
        public int? idservicio { get; set; }
        public DateTime fecha_crear { get; set; }

    }
}
