using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Gradebook
{
    public class CreateGradebookCommand : Entities.Gradebook, IRequest
    {
    }
}
