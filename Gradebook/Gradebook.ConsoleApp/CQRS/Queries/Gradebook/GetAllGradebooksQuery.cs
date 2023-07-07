using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class GetAllGradebooksQuery : IRequest<IEnumerable<Entities.Gradebook>>
    {
    }
}
