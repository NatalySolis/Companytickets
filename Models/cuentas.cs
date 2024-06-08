using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class cuentas
    {
        [Key]
        public int Idcuenta { get; set; }
        public string? Usuario { get; set; }
        public string? Contra { get; set; }
        public int Tipo_usuario { get; set; }

        
    }
}
