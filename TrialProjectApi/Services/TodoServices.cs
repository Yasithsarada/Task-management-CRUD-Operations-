using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using TrialProjectApi.AppDataContext;
using TrialProjectApi.Contracts;
using TrialProjectApi.Interfaces;
using TrialProjectApi.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class TodoService : ITodoInterface               //(ITodoInterface == ITodoService) just names are diffrent
{

    private readonly TodoDbContext _context;           //enables interact with the database
    private readonly ILogger _logger;                  //facilitating logging throughout our application
    private readonly IMapper _mapper;                  //allows to perform object-to-object mapping using AutoMapper

    public TodoService(TodoDbContext context, ILogger<TodoService> logger , IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    //create Todos
    public async Task CreateTodoAsync(CreateTodoRequest request)
    {
        try
        {
            var todo = _mapper.Map<TodoModel>(request);     //MAP THE incoming request to TodoModel
            todo.CreatedAt = DateTime.UtcNow;
            _context.Todos.Add(todo);                       //
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while creating Todo ! Try again ..");
            throw new Exception("An error occured while creating Todo! Try again ..");
        }
    }

    public async Task DeleteTodoAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new Exception( $"Todos are not availble with provided Id" );
        }
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }

    
    //get all Todos
    public async Task<IEnumerable<TodoModel>> GetAllAsync()
    {
        try
        {
            var todo = await _context.Todos.ToListAsync();  //ToListAsync == findAll()
            if (todo == null)
            {
                throw new Exception("No Todos available");
            }
            return todo;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error occured while creating Todo ! Try again ..");
            throw new Exception("An error occured while creating Todo! Try again ..");
        }
        //throw new NotImplementedException();
    }
     
    public async Task<TodoModel> GetByIdAsync(Guid id)
    {
        try
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found ");
            }
            return todo;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "An error occured while finding Todo ! Try again ..");
            throw new Exception("An error occured while finding Todo! Try again ..");
        }
        //throw new NotImplementedException();
    }

    public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
    {

        try
        {

       
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo item with id {id} not found.");
        }

        if (request.Title != null)
        {
            todo.Title = request.Title;
        }
        if (request.DueDate != null)
        {
            todo.DueDate = request.DueDate.Value;
        }
        if (request.Description != null)
        {
             todo.Description = request.Description;    
        }
        if (request.IsComplete != null)
        {
            todo.IsComplete = request.IsComplete.Value;
        }
        if (request.Priority != null)
        {
            todo.priority = request.Priority;
        }
        todo.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        } 
        catch(Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
            
            throw new Exception("An error occured while updating Todo! Try again ..");
        }
    }
}
