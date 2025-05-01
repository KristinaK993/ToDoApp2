using MediatR;

namespace ToDoApp.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<int> //command objekt
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CategoryId {  get; set; }
    }
}
