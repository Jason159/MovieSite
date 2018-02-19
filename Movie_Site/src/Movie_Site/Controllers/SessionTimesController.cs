using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Site.Models;

namespace Movie_Site.Controllers
{
    public class SessionTimesController : Controller
    {
        private readonly Movie_SiteContext _context;

        public SessionTimesController(Movie_SiteContext context)
        {
            _context = context;    
        }

        // GET: SessionTimes
        public async Task<IActionResult> Index()
        {
            var movie_SiteContext = _context.SessionTimes.Include(s => s.Cineplex).Include(s => s.Movie);
            return View(await movie_SiteContext.ToListAsync());
        }

        // GET: SessionTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTimes = await _context.SessionTimes.SingleOrDefaultAsync(m => m.SessionId == id);
            if (sessionTimes == null)
            {
                return NotFound();
            }

            return View(sessionTimes);
        }

        // GET: SessionTimes/Create
        public IActionResult Create()
        {
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Cdescription");
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Mdescription");
            return View();
        }

        // POST: SessionTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionId,BookedSeats,CineplexId,MovieId,MovieTime")] SessionTimes sessionTimes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionTimes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Cdescription", sessionTimes.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Mdescription", sessionTimes.MovieId);
            return View(sessionTimes);
        }

        // GET: SessionTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTimes = await _context.SessionTimes.SingleOrDefaultAsync(m => m.SessionId == id);
            if (sessionTimes == null)
            {
                return NotFound();
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Cdescription", sessionTimes.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Mdescription", sessionTimes.MovieId);
            return View(sessionTimes);
        }

        // POST: SessionTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionId,BookedSeats,CineplexId,MovieId,MovieTime")] SessionTimes sessionTimes)
        {
            if (id != sessionTimes.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionTimes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionTimesExists(sessionTimes.SessionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Cdescription", sessionTimes.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "Mdescription", sessionTimes.MovieId);
            return View(sessionTimes);
        }

        // GET: SessionTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTimes = await _context.SessionTimes.SingleOrDefaultAsync(m => m.SessionId == id);
            if (sessionTimes == null)
            {
                return NotFound();
            }

            return View(sessionTimes);
        }

        // POST: SessionTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionTimes = await _context.SessionTimes.SingleOrDefaultAsync(m => m.SessionId == id);
            _context.SessionTimes.Remove(sessionTimes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SessionTimesExists(int id)
        {
            return _context.SessionTimes.Any(e => e.SessionId == id);
        }
    }
}
