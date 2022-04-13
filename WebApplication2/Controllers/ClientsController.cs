using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ClientsController : Controller
    {
        private readonly WebApplication2Context _context;

        public ClientsController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sorting, string searchingName, string searchingFirm)
        {
            ViewBag.FirstNameSort = String.IsNullOrEmpty(sorting) ? "name_desc" : "";
            ViewBag.LastNameSort = sorting == "name2" ? "name2_desc" : "name2";
            ViewBag.FirmSort = sorting == "firm" ? "firm_desc" : "firm";
            var clientList = from x in _context.Clients select x;
            if (!String.IsNullOrEmpty(searchingName))
            {
                clientList = clientList.Where(x => x.FirstName.Contains(searchingName) || x.LastName.Contains(searchingName));
            }
            if (!String.IsNullOrEmpty(searchingFirm))
            {
                clientList = clientList.Where(x => x.Firm.Contains(searchingFirm));
            }
            switch (sorting)
            {
                case "name_desc":
                    clientList = clientList.OrderByDescending(x => x.FirstName);
                    break;
                case "name2":
                    clientList = clientList.OrderBy(x => x.LastName);
                    break;
                case "name2_desc":
                    clientList = clientList.OrderByDescending(x => x.LastName);
                    break;
                case "firm":
                    clientList = clientList.OrderBy(x => x.Firm);
                    break;
                case "firm_desc":
                    clientList = clientList.OrderByDescending(x => x.Firm);
                    break;
                default:
                    clientList = clientList.OrderBy(x => x.FirstName);
                    break;
            }
            return View(clientList.ToList());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Firm,Email,Phone,Address")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Firm,Email,Phone,Address")] Clients clients)
        {
            if (id != clients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.Id))
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
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clients = await _context.Clients.FindAsync(id);
            var tmp = _context.Orders.FirstOrDefault(x => x.ClientId == id);
            if (tmp != null)
            {
                return View(clients);
            }
            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
