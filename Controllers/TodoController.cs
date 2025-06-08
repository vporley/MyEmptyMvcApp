using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyEmptyMvcApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class TodoController : Controller
{
    // Using dependency injection to access the database
    private readonly AppDbContext _context;

    // Constructor: gets called when the controller is created
    // The framework provides an instance of AppDbContext here
    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Todo
    // This method handles GET requests to show the main Todo list page
    [HttpGet]
    public IActionResult Index()
    {
        // Create a ViewModel and load all Todo items from the database
        var viewModel = new TaskListViewModel
        {
            Tasks = _context.TodoItems.ToList()
        };

        // Pass the ViewModel to the view
        return View(viewModel);
    }

    // POST: /Todo
    // This method handles POST requests when a new item is submitted from the form
    [HttpPost]
    [ValidateAntiForgeryToken] // Helps prevent CSRF attacks
    public IActionResult Index(TaskListViewModel model)
    {
        // Check if the submitted data is valid and NewItem is not null
        if (ModelState.IsValid && model.NewItem != null)
        {
            // Add the new item to the database
            _context.TodoItems.Add(model.NewItem);
            _context.SaveChanges(); // Commit the change
        }

        // Refresh the list of tasks to show updated list
        model.Tasks = _context.TodoItems.ToList();

        // Clear the form by setting NewItem to a new (empty) TodoItem
        model.NewItem = new TodoItem();

        // Return the updated view
        return View(model);
    }

    // GET: /Todo/Add
    // Optional: A separate page for adding a new item (if not doing it on Index)
    public IActionResult Add()
    {
        return View(); // Just returns the Add view
    }

    // POST: /Todo/Add
    // Handles the form submission from the Add view
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(TodoItem item)
    {
        // If the item passes validation rules
        if (ModelState.IsValid)
        {
            _context.TodoItems.Add(item); // Add to database
            _context.SaveChanges(); // Save changes

            // Redirect to the main Index view to show updated list
            return RedirectToAction("Index");
        }

        // If validation fails, return the form view with the submitted data
        return View(item);
    }
}
