using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemaTickets.Models;

namespace Systema_Tickets.Controllers
{
    public class clientesviewController : Controller
    {
        private readonly CompanyContext _context;

        public clientesviewController(CompanyContext context)
        {
            _context = context;
        }

        // GET: clientesview
        public async Task<IActionResult> Index()
        {
            return View(await _context.ticket.ToListAsync());
        }

        // GET: clientesview/Details/5
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

        // GET: clientesview/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: clientesview/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idticket,nombre,descripcion,idcuenta,idpriori,idservicio,fecha_crear")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: clientesview/Edit/5
        

        // POST: clientesview/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        private bool ticketExists(int id)
        {
            return _context.ticket.Any(e => e.idticket == id);
        }
    }
}
