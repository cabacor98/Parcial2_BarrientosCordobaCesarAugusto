using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertDB.DAL;
using ConcertDB.DAL.Entities;

namespace ConcertDB.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ValidateTicket(int id, string entranceGate)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                // Mostrar ventana de notificación de error
                return Content($"<script type='text/javascript'>Swal.fire('Error', 'Boleta no encontrada.', 'error').then(() => window.history.back());</script>");
            }
            else if ((bool)ticket.IsUsed)
            {
                // Mostrar ventana de notificación de error
                return Content($"<script type='text/javascript'>Swal.fire('Error', 'Esta boleta ya fue usada el {ticket.UseDate.Value.ToString("dd/MM/yyyy HH:mm:ss")} en la portería {ticket.EntranceGate}.', 'error').then(() => window.history.back());</script>");
            }
            else
            {
                ticket.IsUsed = true;
                ticket.UseDate = DateTime.Now;
                ticket.EntranceGate = entranceGate;
                _context.SaveChanges();

                // Mostrar ventana de notificación de éxito
                return Content($"<script type='text/javascript'>Swal.fire('Éxito', 'Boleta validada exitosamente.', 'success').then(() => window.history.back());</script>");
            }
        }

    }
}
