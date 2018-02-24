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
    public class MoviesController : Controller
    {
        private readonly Movie_SiteContext _context;

        public MoviesController(Movie_SiteContext context)
        {
            _context = context;    
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,ImageUrl,Mdescription,Price,Title")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,ImageUrl,Mdescription,Price,Title")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }

        private List<Cineplex> getCineplexID(int movieID)
        {
            var matches = _context.SessionTimes.Where(s => s.MovieId == movieID).Select(s => s.CineplexId).Distinct().ToList();
            List<Cineplex> cine = new List<Cineplex>();
            foreach (var m in matches)
            {
                foreach(var c in _context.Cineplex)
                {
                    if(m == c.CineplexId)
                    {
                        cine.Add(c);
                    }
                }
            }

            return cine;
        }

        // CANNOT CONFIRM WORKING
        // Posts movie id and saves to viewbag then shows cineplexes
        [HttpPost]
        public IActionResult selectCineplex(int id)
        {
            // Sets SelectMovie with movieID
            ViewBag.SelectMovie = id;

            var cineplexes = getCineplexID(id);

            return View(cineplexes);
        }
    }
}
