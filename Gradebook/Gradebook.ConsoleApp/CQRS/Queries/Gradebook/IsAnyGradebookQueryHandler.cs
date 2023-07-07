using Gradebook.ConsoleApp.Notifications;
using Gradebook.ConsoleApp.Repositories;
using MediatR;
using System.Security.Policy;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class IsAnyGradebookQueryHandler : IRequestHandler<IsAnyGradebookQuery, bool>
    {
        private readonly IGradebookRepository _repository;

        public IsAnyGradebookQueryHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(IsAnyGradebookQuery request, CancellationToken cancellationToken) =>
            (await _repository.GetAllGradebooks()).Any();
    }
}
