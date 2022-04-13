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
    public class OrdersController : Controller
    {
        private readonly WebApplication2Context _context;

        public OrdersController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string searching)
        {
            var vw = _context.Orders.Include(x => x.Client).Include(x => x.ItemListId).ThenInclude(x => x.IDItems);
            var vwList = await vw.ToListAsync();
            if (!String.IsNullOrEmpty(searching))
            {
                vwList = vwList.FindAll(x => x.Client.FirstName.Contains(searching));
            }
            var vwList2 = vwList.OrderByDescending(x => x.OrderDate);
            return View(vwList2);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tmp = _context.Orders.Include(x => x.Client).Include(x => x.ItemListId).ThenInclude(x => x.IDItems);
            var orders = await tmp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        public IActionResult Create(List<string> itemList, string client)
        {
            var adres = _context.Clients.FirstOrDefault(x => (x.FirstName + " " + x.LastName) == client).Address;
            var ordersCreateModel = new OrdersCreateModel { ItemListConfirm = itemList, ClientConfirm = client, Orders = new Orders { Address = adres } };
            return View(ordersCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Orders orders, string clientConfirm, List<int> itemCountList, List<string> itemListConfirm)
        {
            orders.Client = _context.Clients.FirstOrDefault(x => (x.FirstName + " " + x.LastName) == clientConfirm);
            orders.PriceTotal = 0;
            foreach (var i in itemListConfirm)
            {
                var it = _context.Items.FirstOrDefault(x => x.Name == i);
                var tmp = new OrdersItems { IDOrders = orders, IDItems = it, ItemsCount = itemCountList[itemListConfirm.IndexOf(i)]};
                if(tmp.ItemsCount > tmp.IDItems.ItemCount)
                {
                    return RedirectToAction(nameof(ErrorView));
                }
                orders.PriceTotal += it.Price * tmp.ItemsCount;
                it.ItemCount -= tmp.ItemsCount;
                _context.Add(tmp);
                _context.Update(it);
            }
            orders.PaymentStatus = "Nieopłacone";
            orders.DeliveryStatus = "Niewysłane";
            _context.Add(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateFirst()
        {
            var listItem = from x in _context.Items select x.Name;
            var listClient = from x in _context.Clients select (x.FirstName + " " + x.LastName);
            var primo = new OrdersCreateModel { ItemList = new SelectList(listItem), ClientList = new SelectList(listClient) };
            return View(primo);
        }

        [HttpPost]
        public IActionResult CreateFirst(List<string> itemListConfirm, string ClientConfirm)
        {
            return RedirectToAction(nameof(Create), new { itemList = itemListConfirm, client = ClientConfirm});
        }

        public async Task<IActionResult> CreateSecond(List<string> itemList, string client)
        {
            var ordersCreateModel = new OrdersCreateModel { ItemListConfirm = itemList, ClientConfirm = client};
            return View(ordersCreateModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecond(OrdersCreateModel ordersCreateModel)
        {
            return View(ordersCreateModel);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,Address,PriceTotal,OrderDate,ShipmentDate,DeliveryDate,PaymentStatus,DeliveryStatus,Note")] Orders orders)
        {
            /*if (id != orders.Id)
            {
                return NotFound();
            }*/
            var x = _context.Orders.FirstOrDefault(x => x.Id == id);
            x.Address = orders.Address;
            x.OrderDate = orders.OrderDate;
            x.ShipmentDate = orders.ShipmentDate;
            x.DeliveryDate = orders.DeliveryDate;
            x.PaymentStatus = orders.PaymentStatus;
            x.DeliveryStatus = orders.DeliveryStatus;
            x.Note = orders.Note;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(x);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            var ordit = _context.OrdersItems.Include(x => x.IDOrders);
            foreach (var x in ordit)
            {
                if (x.IDOrders.Id == id)
                {
                    _context.OrdersItems.Remove(x);
                }
            }
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ErrorView()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reports(string filter)
        {
            var orderList = _context.Orders.ToList();
            List<int> lista = new List<int>() { 0,0,0 };
            //[0] = zamówienia, [1] = zamówienia opłacone, [2] = zamówienia dostarczone
            decimal income = 0;
            if(filter == null) { filter = "tydzień"; }
            else { filter = filter.Remove(0,8); }
            int days = 7;
            if(filter == "miesiąc") { days = 31; }
            if(filter == "rok") { days = 365; }
            foreach (var order in orderList)
            {
                if((DateTime.Today - order.OrderDate).Days < days)
                {
                    lista[0] += 1;
                    if (order.PaymentStatus == "Opłacone")
                    { 
                        lista[1] += 1;
                        income += order.PriceTotal;
                    }
                    if (order.DeliveryStatus == "Dostarczone") { lista[2] += 1; }
                }
            }
            var result = new Reports { IncomeTotal = income, Filter = filter, Stats = lista };
            return View(result);
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
