using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestWeek7.Models;
using TestWeek7.Data;
using TestWeek7.Services;

namespace TestWeek7.Controllers
{
    public class TodosController : Controller
    {
        private readonly TestWeek7Context _context;
        private readonly CrudService _crud;

        public TodosController(TestWeek7Context context, CrudService crud)
        {
            _context = context;
            _crud = crud;
        }
       
        // GET: Todos
        public async Task<IActionResult> Index()
        {
             
            return View(await _crud.GetPostsAsync());
        }

        // GET: Todos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _crud.GetPostAsync(id);
               
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET: Todos/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Todo,Completed,UserId")] Todos data)
        {
            if(ModelState.IsValid)
            {
                await _crud.CreatePostAsync(data);

                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        // GET: Todos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todos = await _crud.GetPostAsync(id);
            if (todos == null)
            {
                return NotFound();
            }
            return View(todos);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Todo,Completed,UserId")] Todos todos)
        {
            if (id != todos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _crud.UpdatePostAsync(id, todos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodosExists(todos.Id))
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
            return View(todos);
        }

        // GET: Todos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todos = await _crud.GetPostAsync(id);
               
            if (todos == null)
            {
                return NotFound();
            }

            return View(todos);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _crud.DeletePostAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool TodosExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
