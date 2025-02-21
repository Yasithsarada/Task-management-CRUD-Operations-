using TrialProjectApi.Contracts;
using TrialProjectApi.Model;

namespace TrialProjectApi.Interfaces
{
    public interface ITodoInterface
    {
        Task<IEnumerable<TodoModel>> GetAllAsync();                     //Retrieves all Todo items from the database.
        Task<TodoModel> GetByIdAsync (Guid id);                         //Fetches a specific Todo item by its Id
        Task CreateTodoAsync(CreateTodoRequest request);                //Adds a new Todo item to the database
        Task UpdateTodoAsync(Guid id, UpdateTodoRequest request);       //Modifies an existing Todo item in the database
        Task DeleteTodoAsync(Guid id);                                  //Removes a Todo item from the database
    }
}
