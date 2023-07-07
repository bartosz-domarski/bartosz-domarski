using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class IsAnyGradebookQuery : IRequest<bool>
    {
    }
}
