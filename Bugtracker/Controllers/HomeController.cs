using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Bugtracker.Models;
using Bugtracker.Data;
using Bugtracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Bugtracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _db;

        public HomeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Projects = _db.Projects
            };
            return View(homeVM);
        }

        [Authorize]
        public IActionResult Details(int id)
        {

            DetailsVM DetailsVM = new DetailsVM()
            {
                Project = _db.Projects.Where(u => u.Id == id).FirstOrDefault(),
                Tickets = _db.Tickets.Where(u => u.ProjectId == id).Include(u => u.TicketStatus).Include(u => u.Severity)
            };

            return View(DetailsVM);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Projects.Add(obj.Project);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Projects.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project obj)
        {

            if (ModelState.IsValid)
            {
                _db.Projects.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var tickets = _db.Tickets.Where(u => u.ProjectId == id).Include(u => u.TicketStatus).Include(u => u.Severity);

            DetailsVM DetailsVM = new DetailsVM()
            {
                Project = _db.Projects
                .Where(u => u.Id == id).FirstOrDefault(),
                Tickets = tickets
            };

            return View(DetailsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Projects.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Projects.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
