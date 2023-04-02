using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2_FlorezRSebastian.DAL;
using Parcial2_FlorezRSebastian.DAL.Entities;

namespace Parcial2_FlorezRSebastian.Controllers
{
    public class TicketsController : Controller
    {
        #region Constructor
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }
        #endregion

        #region Private Methods
        private async Task<Ticket> GetTicketById(Guid? Id)
        {
           
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == Id);              
              
        }

        #endregion

        #region Ticket Actions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets
                
                .ToListAsync());
        }

        public IActionResult Create() //actionresult retornar cualquier tipo de cosa
        {
            return View(); //retorna lo que se desee en view es una vista, en file un tipo de archivo
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)//task es una tarea que realiza una promesa; <tipo devuelve>
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.UsedDate = DateTime.Now;
                    _context.Add(ticket);
                    await _context.SaveChangesAsync(); //savechangeasync es para hacer en base de datos el insert into, await permite continuar en segundo plano
                    return RedirectToAction(nameof(Index)); //devuelve despues de realizar accion devuekver a la pestaña especifica de index
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un ticket con el mismo numero.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(ticket);
        }

        public async Task<IActionResult> Edit(Guid? Id)
        {
            if (Id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket= await GetTicketById(Id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost] //hacer una insercion en base de datos
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Ticket ticket) //task es una tarea que realiza una promesa; <tipo devuelve>
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.UsedDate = DateTime.Now;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync(); //asyncronica es algo se ejecuta en segundo plano, sync es esperar a terminar esa accion. para consultas en base datos usa async await
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un tiquete con el mismo numero.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(ticket);
        }

        public async Task<IActionResult> Details(Guid? Id)
        {
            if (Id == null || _context.Tickets == null) return NotFound();

            var ticket = await _context.Tickets
          
                .FirstOrDefaultAsync(t => t.Id ==  Id);

            if (ticket == null) return NotFound();

            return View(ticket);
        }

        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets
               
                .FirstOrDefaultAsync(t => t.Id == Id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'DatabaseContext.Countries' is null.");
            }
            var ticket = await _context.Tickets
               
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
#endregion
        
    }
}
