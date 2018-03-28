using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Site.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

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


        // Takes movie ID, matches with session times and returns a list of matching cineplexes
        private List<Cineplex> GetCineplexID(int movieID)
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

        // Takes movie ID and returns Title
        private string GetTitle(int id)
        {
            Debug.WriteLine("getTitle: " + id);
            var movie = _context.Movie.Where(m => m.MovieId == id).FirstOrDefault();

            Debug.WriteLine("Movie: " + movie);
            var title = movie.Title;

            return title;
        }

        // Takes cine ID and returns location
        private string GetLoc(int id)
        {
            Debug.WriteLine("getCine: " + id);
            var cine = _context.Cineplex.Where(c => c.CineplexId == id).FirstOrDefault();

            Debug.WriteLine("Cine: " + cine);
            var loc = cine.Location;

            return loc;
        }

        // Posts movie id and saves to viewbag then shows cineplexes
        [HttpPost]
        public IActionResult SelectCineplex(int id)
        {
            // Sets session for MovieID and Title
            Debug.WriteLine("POST id: "+id);
            HttpContext.Session.SetInt32("MovieID", id);
            HttpContext.Session.SetString("Title", GetTitle(id));


            // Sets Viewbag.MovieTitle to be used in view            
            ViewBag.MovieTitle = HttpContext.Session.GetString("Title");

            //Gets model of cineplexes that display the selected movie
            var cineplexes = GetCineplexID(id);

            return View(cineplexes);
        }

        // Shows cineplex selection when movieID session is already set
        // Needs exception handling in case manually nav
        public IActionResult SelectCineplex()
        {
            var id = HttpContext.Session.GetInt32("MovieID").Value;
            var cineplexes = GetCineplexID(id);

            // Sets Viewbag.MovieTitle to be used in view            
            ViewBag.MovieTitle = HttpContext.Session.GetString("Title");

            return View(cineplexes);
        }

        // Sets cineplex and redirects to session times
        [HttpPost]
        public IActionResult SetCineplex(int id)
        {
            // Sets session for CineplexID and location
            Debug.WriteLine("POST id: " + id);
            HttpContext.Session.SetInt32("CineplexID", id);
            HttpContext.Session.SetString("Location", GetLoc(id));
                        
            return RedirectToAction("Index", "SessionTimes");
        }
    }
}
