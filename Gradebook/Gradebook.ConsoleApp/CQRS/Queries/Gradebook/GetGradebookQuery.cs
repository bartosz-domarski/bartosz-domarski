using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class GetGradebookQuery : IRequest<Entities.Gradebook>
    {
        public string StudentName { get; set; } = default!;
    }
}
