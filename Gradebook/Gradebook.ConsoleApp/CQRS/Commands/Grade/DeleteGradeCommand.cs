using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Grade
{
    public class DeleteGradeCommand : IRequest
    {
        public Entities.Gradebook Gradebook { get; set; } = default!;
        public Entities.Grade Grade { get; set; } = default!;
    }
}
