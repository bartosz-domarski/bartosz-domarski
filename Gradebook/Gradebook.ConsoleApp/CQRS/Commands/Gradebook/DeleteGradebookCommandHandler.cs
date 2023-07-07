using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Gradebook
{
    public class DeleteGradebookCommandHandler : IRequestHandler<DeleteGradebookCommand>
    {
        private readonly IGradebookRepository _repository;

        public DeleteGradebookCommandHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGradebookCommand request, CancellationToken cancellationToken) =>
            await _repository.DeleteGradebook(request);
    }
}
