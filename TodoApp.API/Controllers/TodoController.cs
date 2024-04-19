using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoDbContext.Todos
                .Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();

            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, Todo todoUpdateRequest)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if(todo == null)
                return NotFound();
            

            todo.IsCompleted = todoUpdateRequest.IsCompleted;
            todo.CompletedDate = DateTime.Now;

            await _todoDbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut]
        [Route("undo-deleted-todo/{id:Guid}")]
        public async Task<IActionResult> UndoDeletedTodo([FromRoute] Guid id, Todo undoDeleteTodoRequest)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                return NotFound();

            todo.DeletedDate = null;
            todo.isDeleted = false;

            await _todoDbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);

            if (todo == null)
                return NotFound();

            todo.isDeleted = true;
            todo.DeletedDate = DateTime.Now;

            await _todoDbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpGet]
        [Route("get-deleted-todos")]
        public async Task<IActionResult> GetAllDeletedTodos()
        {
            var deletedTodos = await _todoDbContext.Todos
                .Where(x => x.isDeleted == true)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(deletedTodos);
        }

    }
}

