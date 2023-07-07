using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Commands.Gradebook
{
    public class CreateGradebookCommandHandler : IRequestHandler<CreateGradebookCommand>
    {
        private readonly IGradebookRepository _repository;

        public CreateGradebookCommandHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGradebookCommand request, CancellationToken cancellationToken) =>
            await _repository.AddGradebook(request);
    }
}
