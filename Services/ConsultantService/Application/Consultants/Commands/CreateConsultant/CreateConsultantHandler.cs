using Application.Common.Persistence;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Commands.CreateConsultant;

public sealed class CreateConsultantHandler : IRequestHandler<CreateConsultantCmd, Result<Guid>>
{
    private readonly IConsultantRepository _consultantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateConsultantHandler(IConsultantRepository consultantRepository, IUnitOfWork unitOfWork)
    {
        _consultantRepository = consultantRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(CreateConsultantCmd request, CancellationToken cancellationToken)
    {
        var make = Consultant.Create(request.FirstName, request.LastName, request.Email, request.Speciality);

        if (make.IsFailure) { return Result<Guid>.Failure(make.Error); }

        await _consultantRepository.AddAsync(make.Value, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<Guid>.Success(make.Value.Id);



    }
}
