using Domain.Shared;
using MediatR;

namespace ConsultantService.Application.Consultants.Commands.CreateConsultantFromEvent;

public sealed record CreateConsultantFromEventCommand(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string Speciality) : IRequest<Result<Domain.Shared.Unit>>;
