using Microsoft.AspNetCore.Mvc;
using ProyectoSistemaTickets.Models;

namespace ProyectoSistemaTickets.Controllers
{
    public class LoginController : Controller
    {
        private readonly CompanyContext _context;

        public LoginController(CompanyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contra)
        {
            var cuenta = _context.cuentas.SingleOrDefault(c => c.Usuario == usuario && c.Contra == contra);
            if (cuenta == null)
            {
                // Maneja el caso donde la autenticación falla
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
                return View("Index"); // Asume que tienes una vista de inicio de sesión
            }

            if (cuenta.Tipo_usuario == 1)
            {
                // Redirige al controlador de administrador
                return RedirectToAction("Index", "tickets");
            }
            if (cuenta.Tipo_usuario == 2)
            {
                // Redirige al controlador de administrador
                return RedirectToAction("Index", "tickets");
            }
            else
            {
                // Redirige al controlador de cliente
                return RedirectToAction("Index", "clientesview");
            }
        }

    }
}
