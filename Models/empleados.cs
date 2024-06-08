using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class empleados
    {
        [Key]
        public int idempleado {  get; set; }
        public string nombre { get; set; }
        public string tel {  get; set; }
        public int idrol { get; set; }
        public int idcuenta { get; set; }

    }
}
