using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeisterMask.Data;
using TeisterMask.Models;

namespace TeisterMask.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (var db = new TeisterMaskDbContext())
            {
                var task = db.Tasks.ToList();
                return this.View(task);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(string title, string status)
        {
            if (string.IsNullOrEmpty(title))
            {
                return RedirectToAction("Index");
            }

            Task task = new Task
            {
                Title = title,
                Status = status
            };

            using (var db = new TeisterMaskDbContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new TeisterMaskDbContext())
            {
                var taskToChange = db.Tasks.FirstOrDefault(t => t.Id == id);
                if (taskToChange == null)
                {
                    return RedirectToAction("Index");
                }
                return View(taskToChange);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id,string title, string status)
        {
            using (var db = new TeisterMaskDbContext())
            {
                var taskToChange = db.Tasks.FirstOrDefault(t => t.Id == id);
                db.Tasks.Remove(taskToChange);

                Task task = new Task
                {
                    Title = title,
                    Status = status
                };

                db.Tasks.Add(task);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            using (var db = new TeisterMaskDbContext())
            {
                var taskToRemove = db.Tasks.FirstOrDefault(t => t.Id == id);
                return View(taskToRemove);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id,string title,string status)
        {
            using (var db = new TeisterMaskDbContext())
            {
                var taskToRemove = db.Tasks.FirstOrDefault(t => t.Id == id);
                db.Tasks.Remove(taskToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
