using Microsoft.EntityFrameworkCore;

namespace ProyectoSistemaTickets.Models
{
    public class CompanyContext : DbContext
    {

        public CompanyContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<clientes> clientes { get; set; }
        public DbSet<ticket> ticket { get; set; }
        public DbSet<detalle_ticket> detalle_ticket { get; set; }
        public DbSet<empleados> empleados { get; set; }
        public DbSet<estados> estados { get; set; }
        public DbSet<rol> rol { get; set; }
        public DbSet<cuentas> cuentas { get; set; }
        public DbSet<prioridad> prioridad { get; set; }
        public DbSet<tipo_servicio> tipo_servicio { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
    }
}
