using System.ComponentModel.DataAnnotations;

namespace ProyectoSistemaTickets.Models
{
    public class comentarios
    {
        [Key]
        public int idcometario { get; set; }
        public int id_detalle { get; set; }
        public string comentar {  get; set; }

    }
}
