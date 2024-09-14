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
    public class FundingRequestsController : Controller
    {
        private readonly BumbleBeeDbContext _context;

        public FundingRequestsController(BumbleBeeDbContext context)
        {
            _context = context;
        }

        // GET: FundingRequests
        public async Task<IActionResult> Index()
        {
            var bumbleBeeDbContext = _context.FundingRequests.Include(f => f.Company);
            return View(await bumbleBeeDbContext.ToListAsync());
        }

        // GET: FundingRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingRequest = await _context.FundingRequests
                .Include(f => f.Company)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (fundingRequest == null)
            {
                return NotFound();
            }

            return View(fundingRequest);
        }

        // GET: FundingRequests/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            return View();
        }

        // POST: FundingRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,CompanyId,ProjectDescription,RequestedAmount,ProjectImpact,Status,SubmittedAt")] FundingRequest fundingRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fundingRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", fundingRequest.CompanyId);
            return View(fundingRequest);
        }

        // GET: FundingRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingRequest = await _context.FundingRequests.FindAsync(id);
            if (fundingRequest == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", fundingRequest.CompanyId);
            return View(fundingRequest);
        }

        // POST: FundingRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,CompanyId,ProjectDescription,RequestedAmount,ProjectImpact,Status,SubmittedAt")] FundingRequest fundingRequest)
        {
            if (id != fundingRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fundingRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FundingRequestExists(fundingRequest.RequestId))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", fundingRequest.CompanyId);
            return View(fundingRequest);
        }

        // GET: FundingRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fundingRequest = await _context.FundingRequests
                .Include(f => f.Company)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (fundingRequest == null)
            {
                return NotFound();
            }

            return View(fundingRequest);
        }

        // POST: FundingRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fundingRequest = await _context.FundingRequests.FindAsync(id);
            if (fundingRequest != null)
            {
                _context.FundingRequests.Remove(fundingRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FundingRequestExists(int id)
        {
            return _context.FundingRequests.Any(e => e.RequestId == id);
        }
    }
}
