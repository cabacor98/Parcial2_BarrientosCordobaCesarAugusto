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

        public ActionResult ValidateTicket()
        {
            return View();
        }

        public IActionResult ValidateTicket(int id, string entranceGate)
        {
            var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return Json(new { success = false, message = "Boleta no válida" });
            }
            else if ((bool)ticket.IsUsed)
            {
                return Json(new { success = false, message = $"Esta boleta ya fue usada el {ticket.UseDate.Value.ToString("dd/MM/yyyy HH:mm:ss")} por la portería {ticket.EntranceGate}" });
            }
            else
            {
                ticket.IsUsed = true;
                ticket.UseDate = DateTime.Now;
                ticket.EntranceGate = entranceGate;
                _context.SaveChanges();

                return Json(new { success = true, message = "Boleta válida" });
            }
        }
    }

}
