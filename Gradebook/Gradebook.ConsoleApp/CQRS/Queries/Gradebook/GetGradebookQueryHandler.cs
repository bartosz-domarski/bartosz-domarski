using Gradebook.ConsoleApp.Repositories;
using MediatR;

namespace Gradebook.ConsoleApp.CQRS.Queries.Gradebook
{
    public class GetGradebookQueryHandler : IRequestHandler<GetGradebookQuery, Entities.Gradebook>
    {
        private readonly IGradebookRepository _repository;

        public GetGradebookQueryHandler(IGradebookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Entities.Gradebook> Handle(GetGradebookQuery request, CancellationToken cancellationToken) =>
            await _repository.GetGradebook(request.StudentName);
    }
}
