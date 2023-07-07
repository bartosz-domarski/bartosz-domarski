using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Gradebook
{
    public class DeleteGradebookCommand : Entities.Gradebook, IRequest
    {
    }
}
