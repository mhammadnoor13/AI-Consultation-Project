using Application.Common.Persistence;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace ConsultantService.Application.Consultants.Commands.CreateConsultantFromEvent;

public sealed class CreateConsultantFromEventCommandHandler
    : IRequestHandler<CreateConsultantFromEventCommand, Result<Domain.Shared.Unit>>
{
    private readonly IConsultantRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateConsultantFromEventCommandHandler(IConsultantRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<Domain.Shared.Unit>> Handle(CreateConsultantFromEventCommand cmd, CancellationToken ct)
    {
        var consultant = Consultant.Create(cmd.FirstName, cmd.LastName, cmd.Email, cmd.Speciality);
        consultant.Value.Id = cmd.Id;
        if (consultant.IsFailure)
            return Result<Domain.Shared.Unit>.Failure(consultant.Error);

        await _repo.AddAsync(consultant.Value, ct);
        await _uow.CommitAsync(ct);

        return Result<Domain.Shared.Unit>.Success(Domain.Shared.Unit.Value);
    }
}
