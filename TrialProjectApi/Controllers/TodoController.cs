using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrialProjectApi.Contracts;
using TrialProjectApi.Interfaces;
using TrialProjectApi.Model;

namespace TrialProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoInterface _todoServices;
        public TodoController(ITodoInterface todoService)
        {
            _todoServices = todoService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)                    //model validation
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "  Todo successfully Added !" });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });
            }
                
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if (todo == null || !todo.Any())
                {
                    return Ok(new { message = "No todos available" });
                }
                return Ok(new { message = "Successfully retrived all Todos", data = todo });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message ="An error occured while retriveiing todos" });
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = todo });

            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}", error = ex.Message });
            }

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id , UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todos are not availble with provided Id" });
                }
                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $"Todo Item  with id {id} successfully updated" });
            }
            catch (Exception ex) 
            {
                return StatusCode(500 , new { ex , message = $"An error occurred while updating blog post with id {id}" , error =ex.Message} );
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"Todos are not availble with provided Id" });
                }
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = $"Todo successfully deleted" });
            }
            catch( Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });
            }
        }
    }
}
