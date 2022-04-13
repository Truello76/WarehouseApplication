using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ItemsController : Controller
    {
        private readonly WebApplication2Context _context;

        public ItemsController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Items
        [Authorize]
        public async Task<IActionResult> Index(string sorting, string searching, string searchingShelf)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sorting) ? "name_desc" : "";
            ViewBag.CountSort = sorting == "count" ? "count_desc" : "count";
            ViewBag.PriceSort = sorting == "price" ? "price_desc" : "price";
            ViewBag.ShelfSort = sorting == "shelf" ? "shelf_desc" : "shelf";
            var itemList = from x in _context.Items select x;
            if (!String.IsNullOrEmpty(searching))
            {
                itemList = itemList.Where(x => x.Name.Contains(searching));
            }
            if (!String.IsNullOrEmpty(searchingShelf))
            {
                itemList = itemList.Where(x => x.ShelfCode.Contains(searchingShelf));
            }
            switch (sorting)
            {
                case "name_desc":
                    itemList = itemList.OrderByDescending(x => x.Name);
                    break;
                case "count":
                    itemList = itemList.OrderBy(x => x.ItemCount);
                    break;
                case "count_desc":
                    itemList = itemList.OrderByDescending(x => x.ItemCount);
                    break;
                case "price":
                    itemList = itemList.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    itemList = itemList.OrderByDescending(x => x.Price);
                    break;
                case "shelf":
                    itemList = itemList.OrderBy(x => x.ShelfCode);
                    break;
                case "shelf_desc":
                    itemList = itemList.OrderByDescending(x => x.ShelfCode);
                    break;
                default:
                    itemList = itemList.OrderBy(x => x.Name);
                    break;
            }    
            return View(itemList.ToList());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ItemCount,Price,ShelfCode,Fragile")] Items items)
        {
            if (ModelState.IsValid)
            {
                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ItemCount,Price,ShelfCode,Fragile")] Items items)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.Id))
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
            return View(items);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.Items.FindAsync(id);
            var tmp = _context.OrdersItems.FirstOrDefault(x => x.IDItems.Id == id);
            if (tmp != null)
            {
                return RedirectToAction(nameof(ErrorView));
            }
            _context.Items.Remove(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ErrorView()
        {
            return View();
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
