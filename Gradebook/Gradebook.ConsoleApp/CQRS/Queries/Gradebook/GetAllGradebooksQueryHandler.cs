using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class GetAllGradebooksQueryHandler : IRequestHandler<GetAllGradebooksQuery, IEnumerable<Entities.Gradebook>>
    {
        private readonly IGradebookRepository _repository;

        public GetAllGradebooksQueryHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Entities.Gradebook>> Handle(GetAllGradebooksQuery request, CancellationToken cancellationToken) =>
            await _repository.GetAllGradebooks();

    }
}
