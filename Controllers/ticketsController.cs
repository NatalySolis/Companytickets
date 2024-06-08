using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemaTickets.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Systema_Tickets.Controllers
{
    public class ticketsController : Controller
    {
        private readonly CompanyContext _context;
        private readonly IConfiguration _configuration;

        public ticketsController(CompanyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.ticket.ToListAsync());
        }

        // GET: tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket
                .FirstOrDefaultAsync(m => m.idticket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: tickets/Create
        public IActionResult Create()
        {
            return View();
        }


        // Método para enviar correo electrónico
        private async Task EnviarCorreoAsync(string destinatario, string asunto, string mensaje)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("HelpDesk", _configuration["Smtp:Username"]));
            emailMessage.To.Add(new MailboxAddress("", destinatario));
            emailMessage.Subject = asunto;
            emailMessage.Body = new TextPart("plain") { Text = mensaje };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }


        // POST: tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idticket,nombre,descripcion,idcuenta,idpriori,idservicio,fecha_crear")] ticket ticket)
        {
            if (ModelState.IsValid)
            {

                // Enviar correo electrónico al usuario que creó el ticket
                var user = await _context.clientes.FindAsync(ticket.idcuenta);
                if (user != null && !string.IsNullOrEmpty(user.correo))
                {
                    string asunto = ticket.nombre;
                    string mensaje = $"Se ha creado un nuevo ticket con los siguientes detalles:\n\n" +
                                     $"Título: {ticket.nombre}\n" +
                                     $"Descripción: {ticket.descripcion}\n\n" +
                                     "Pronto será contactado por un soporte.";
                    await EnviarCorreoAsync(user.correo, asunto, mensaje);
                }


                // Enviar correo electrónico al admin o soporte
                string adminEmail = "naty76181@gmail.com";
                string asuntoAdmin = $"Nuevo ticket asignado: {ticket.nombre}";
                string mensajeAdmin = $"Se ha creado un nuevo ticket con los siguientes detalles:\n\n" +
                                      $"Título: {ticket.nombre}\n" +
                                      $"Descripción: {ticket.descripcion}\n" +
                                      $"Asignado a: Administrador\n\n" +
                                      $"El ticket fue creado por: {user?.nombre}.";
                await EnviarCorreoAsync(adminEmail, asuntoAdmin, mensajeAdmin);


                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));




            }
            return View(ticket);
        }

        // GET: tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idticket,nombre,descripcion,idcuenta,idpriori,idservicio,fecha_crear")] ticket ticket)
        {
            if (id != ticket.idticket)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ticketExists(ticket.idticket))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket
                .FirstOrDefaultAsync(m => m.idticket == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ticketExists(int id)
        {
            return _context.ticket.Any(e => e.idticket == id);
        }
    }
}
