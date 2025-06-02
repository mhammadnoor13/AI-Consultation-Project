using MediatR;

namespace ConsultantService.Application.Consultants.Commands.AssignCase;
public sealed record AssignCaseCommand(Guid CaseId, string Speciality)
    : IRequest;