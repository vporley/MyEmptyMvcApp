using Microsoft.AspNetCore.Mvc;
using MyEmptyMvcApp.Models;

namespace MyEmptyMvcApp.Controllers
{
    // Controller for managing Todo tasks
    public class TodoController : Controller
    {
        private readonly AppDbContext _context; // Database context

        // Constructor: Injects the AppDbContext
        public TodoController(AppDbContext context) => _context = context;

        // GET: /Todo/Add
        // Shows the empty form
        public IActionResult Index()
        {
            return View(); // Renders the Add.cshtml view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item); // Show form again with error messages
            }

            _context.TodoItems.Add(item); // Add to the database
            _context.SaveChanges();       // Save changes permanently

            return RedirectToAction("Index");  // Go back to the task list
        }
    }
}
