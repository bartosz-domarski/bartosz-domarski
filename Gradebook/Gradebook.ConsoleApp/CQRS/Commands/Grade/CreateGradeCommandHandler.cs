using Gradebook.ConsoleApp.Entities;
using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Grade
{
    public class CreateGradeCommandHandler : Entities.Grade, IRequestHandler<CreateGradeCommand>
    {
        private readonly IGradebookRepository _repository;

        public CreateGradeCommandHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddGrade(request.Gradebook, request.Grade);
        }
    }
}
