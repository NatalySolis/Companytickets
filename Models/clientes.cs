using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class clientes
    {
        [Key]
        public int idcliente { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string nombre_empresa { get; set; }
        public string contacto_principal { get; set; }
        public string direc {  get; set; }
        public string tel { get; set; }
        public int idcuenta { get; set; }

    }
}
