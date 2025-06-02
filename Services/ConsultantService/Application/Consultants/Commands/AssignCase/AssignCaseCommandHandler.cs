using Application.Common.Persistence;
using MediatR;

namespace ConsultantService.Application.Consultants.Commands.AssignCase
{
    public sealed class AssignCaseCommandHandler : IRequestHandler<AssignCaseCommand>
    {
        private readonly IConsultantRepository _repo;

        public AssignCaseCommandHandler(IConsultantRepository repo) => _repo = repo;

        public async Task Handle(AssignCaseCommand cmd, CancellationToken ct)
        {
            var consultant = await _repo.GetBySpecialityAsync(cmd.Speciality, ct);
            if (consultant.Value is null)
                throw new InvalidOperationException(
                    $"No consultant registered for speciality '{cmd.Speciality}'.");

            await _repo.AppendCaseAsync(consultant.Value.Id, cmd.CaseId, ct);
        }
    }
}
