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

        public class ResponseDto
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public IActionResult ValidateTicket(int id, string entranceGate)
        {
            var ticket = _context.Tickets.Find(id);


            var response = new ResponseDto();

            switch (ticket)
            {
                case null:
                    response.Success = false;
                    response.Message = "Boleta no encontrada.";
                    break;

                case { IsUsed: true }:
                    response.Success = false;
                    response.Message = $"Esta boleta ya fue usada el {ticket.UseDate.Value.ToString("dd/MM/yyyy HH:mm:ss")} en la portería {ticket.EntranceGate}.";
                    break;

                default:
                    ticket.IsUsed = true;
                    ticket.UseDate = DateTime.Now;
                    ticket.EntranceGate = entranceGate;
                    _context.SaveChanges();
                    response.Success = true;
                    response.Message = "Boleta validada exitosamente.";
                    break;
            }

            return Json(response);
        }

    }
}
