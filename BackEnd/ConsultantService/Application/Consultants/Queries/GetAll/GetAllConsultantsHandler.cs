using Application.Common.Persistence;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Consultants.Queries.GetAll;

public sealed class GetAllConsultantsHandler
    : IRequestHandler<GetAllConsultantsQuery, Result<IEnumerable<Consultant>>>
{
    private readonly IConsultantRepository _repo;

    public GetAllConsultantsHandler(IConsultantRepository repo)
        => _repo = repo;

    public async Task<Result<IEnumerable<Consultant>>> Handle(
        GetAllConsultantsQuery request,
        CancellationToken ct)
    {
        // Calls your MongoConsultantRepository.GetAllAsync() :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}
        return await _repo.GetAllAsync(ct);
    }
}