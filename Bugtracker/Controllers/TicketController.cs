using System.Collections.Generic;
using System.Linq;
using Bugtracker.Data;
using Bugtracker.Models;
using Bugtracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDBContext _db;
        public TicketController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TicketModel> ticketList = _db.Tickets.Include(u => u.TicketStatus).Include(u => u.Severity).Include(u => u.Project);

            return View(ticketList);
        }

        [Authorize]
        public IActionResult Upsert(int? id)
        {
            TicketVM ticketVM = new TicketVM()
            {
                TicketModel = new TicketModel(),
                SeverityList = _db.Severities.Select(i => new SelectListItem
                {
                    Text = i.severityLevel,
                    Value = i.Id.ToString()
                }),
                StatusList = _db.Status.Select(i => new SelectListItem
                {
                    Text = i.currentStatus,
                    Value = i.Id.ToString()
                }),
                AvailableProjects = _db.Projects.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(ticketVM);
            }
            else
            {
                ticketVM.TicketModel = _db.Tickets.Find(id);
                if (ticketVM.TicketModel == null)
                {
                    return NotFound();
                }
                return View(ticketVM);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TicketVM ticketVM)
        {
            if (ModelState.IsValid)
            {

                if (ticketVM.TicketModel.Id == 0)
                {
                    _db.Tickets.Add(ticketVM.TicketModel);
                }
                else
                {

                    var objFromDb = _db.Tickets.AsNoTracking().FirstOrDefault(u => u.Id == ticketVM.TicketModel.Id);

                    _db.Tickets.Update(ticketVM.TicketModel);
                }


                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ticketVM.SeverityList = _db.Severities.Select(i => new SelectListItem
            {
                Text = i.severityLevel,
                Value = i.Id.ToString()
            });
            ticketVM.StatusList = _db.Status.Select(i => new SelectListItem
            {
                Text = i.currentStatus,
                Value = i.Id.ToString()
            });
            ticketVM.AvailableProjects = _db.Projects.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            return View(ticketVM);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            TicketModel ticket = _db.Tickets.Include(u => u.Severity).Include(u => u.TicketStatus).Include(u => u.Project).FirstOrDefault(u => u.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Tickets.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Tickets.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
