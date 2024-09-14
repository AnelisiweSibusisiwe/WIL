using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BumbleBeeFoundation.Models;

namespace BumbleBeeFoundation.Controllers
{
    public class DonationSarsController : Controller
    {
        private readonly BumbleBeeDbContext _context;

        public DonationSarsController(BumbleBeeDbContext context)
        {
            _context = context;
        }

        // GET: DonationSars
        public async Task<IActionResult> Index()
        {
            var bumbleBeeDbContext = _context.DonationSars.Include(d => d.Donation);
            return View(await bumbleBeeDbContext.ToListAsync());
        }

        // GET: DonationSars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSar = await _context.DonationSars
                .Include(d => d.Donation)
                .FirstOrDefaultAsync(m => m.Sarsid == id);
            if (donationSar == null)
            {
                return NotFound();
            }

            return View(donationSar);
        }

        // GET: DonationSars/Create
        public IActionResult Create()
        {
            ViewData["DonationId"] = new SelectList(_context.Donations, "DonationId", "DonationId");
            return View();
        }

        // POST: DonationSars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sarsid,DonationId,GeneratedDate,Sarsdocument")] DonationSar donationSar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donationSar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonationId"] = new SelectList(_context.Donations, "DonationId", "DonationId", donationSar.DonationId);
            return View(donationSar);
        }

        // GET: DonationSars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSar = await _context.DonationSars.FindAsync(id);
            if (donationSar == null)
            {
                return NotFound();
            }
            ViewData["DonationId"] = new SelectList(_context.Donations, "DonationId", "DonationId", donationSar.DonationId);
            return View(donationSar);
        }

        // POST: DonationSars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sarsid,DonationId,GeneratedDate,Sarsdocument")] DonationSar donationSar)
        {
            if (id != donationSar.Sarsid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationSar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationSarExists(donationSar.Sarsid))
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
            ViewData["DonationId"] = new SelectList(_context.Donations, "DonationId", "DonationId", donationSar.DonationId);
            return View(donationSar);
        }

        // GET: DonationSars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationSar = await _context.DonationSars
                .Include(d => d.Donation)
                .FirstOrDefaultAsync(m => m.Sarsid == id);
            if (donationSar == null)
            {
                return NotFound();
            }

            return View(donationSar);
        }

        // POST: DonationSars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationSar = await _context.DonationSars.FindAsync(id);
            if (donationSar != null)
            {
                _context.DonationSars.Remove(donationSar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationSarExists(int id)
        {
            return _context.DonationSars.Any(e => e.Sarsid == id);
        }
    }
}
