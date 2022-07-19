using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ispit.Todo.Data;
using Ispit.Todo.Models.Dbo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Ispit.Todo.Controllers
{
    [Authorize]
    public class TodoListsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AspNetUser> userManager;



        public TodoListsController(ApplicationDbContext context, UserManager<AspNetUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ListIndex()
        {
            return context.TodoList != null ?
                        View(await context.TodoList.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.TodoList'  is null.");
        }

        public async Task<IActionResult> ListDetails(int? id)
        {
            if (id == null || context.TodoList == null)
            {
                return NotFound();
            }

            var todoList = await context.TodoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        public IActionResult CreateList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList([Bind("Id,Created,ListName")] TodoList todoList)
        {
            var user = await userManager.GetUserAsync(User);
            todoList.AspNetUser = user;
            context.Add(todoList);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ListIndex));
        }

        public async Task<IActionResult> EditList(int? id)
        {
            if (id == null || context.TodoList == null)
            {
                return NotFound();
            }

            var todoList = await context.TodoList.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }
            return View(todoList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditList(int id, [Bind("Id,Created,ListName")] TodoList todoList)
        {

            context.Update(todoList);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ListIndex));

        }

        public async Task<IActionResult> DeleteList(int? id)
        {
            if (id == null || context.TodoList == null)
            {
                return NotFound();
            }

            var todoList = await context.TodoList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        [HttpPost, ActionName("DeleteList")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteListConfirmed(int id)
        {
            if (context.TodoList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TodoList'  is null.");
            }
            var todoList = await context.TodoList.FindAsync(id);
            if (todoList != null)
            {
                context.TodoList.Remove(todoList);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ListIndex));
        }

        private bool TodoListExists(int id)
        {
            return (context.TodoList?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> TaskIndex()
        {
            
            return View(await context.TodoTask.Where(x => x.Status == false).ToListAsync());
        }

        public async Task<IActionResult> TaskDetails(int? id)
        {
            if (id == null || context.TodoTask == null)
            {
                return NotFound();
            }

            var todoTask = await context.TodoTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }

            return View(todoTask);
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask([Bind("Id,Created,TaskName,Status")] TodoTask todoTask)
        {
            
            context.Add(todoTask);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(TaskIndex));
        }

        public async Task<IActionResult> EditTask(int? id)
        {
            if (id == null || context.TodoTask == null)
            {
                return NotFound();
            }

            var todoTask = await context.TodoTask.FindAsync(id);
            if (todoTask == null)
            {
                return NotFound();
            }
            return View(todoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(int id, [Bind("Id,Created,TaskName,Status")] TodoTask todoTask)
        {

            context.Update(todoTask);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(TaskIndex));

        }

        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (id == null || context.TodoTask == null)
            {
                return NotFound();
            }

            var todoTask = await context.TodoTask
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }

            return View(todoTask);
        }

        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            if (context.TodoTask == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TodoTask'  is null.");
            }
            var todoTask = await context.TodoTask.FindAsync(id);
            if (todoTask != null)
            {
                context.TodoTask.Remove(todoTask);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(TaskIndex));
        }

        private bool TodoTaskExists(int id)
        {
            return (context.TodoTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
