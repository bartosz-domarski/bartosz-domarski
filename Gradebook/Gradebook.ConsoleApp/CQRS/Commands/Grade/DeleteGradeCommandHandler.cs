using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Grade
{
    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand>
    {
        private readonly IGradebookRepository _repository;

        public DeleteGradeCommandHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGradeCommand request, CancellationToken cancellationToken) =>
            await _repository.DeleteGrade(request.Gradebook, request.Grade);
    }
}
