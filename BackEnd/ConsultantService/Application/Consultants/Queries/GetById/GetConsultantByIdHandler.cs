using Application.Common.Persistence;
using Application.Consultants.DTOs;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Queries.GetById;

public sealed class GetConsultantByIdHandler : IRequestHandler<GetConsultantByIdQuery, Result<ConsultantResponse>>
{
    private readonly IConsultantRepository _consultantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetConsultantByIdHandler(IConsultantRepository consultantRepository , IUnitOfWork unitOfWork)
    {
        _consultantRepository   = consultantRepository;
        _unitOfWork = unitOfWork;   
    }

    public async Task<Result<ConsultantResponse>> Handle(GetConsultantByIdQuery request, CancellationToken cancellationToken)
    {
        var res = await _consultantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (res.IsFailure) { return Result<ConsultantResponse>.Failure(res.Error); }

        var consultant = res.Value;

        return Result<ConsultantResponse>.Success(
            new ConsultantResponse(consultant.Id, $"{consultant.FirstName} {consultant.LastName}", consultant.Speciality)
            );

    }
}
