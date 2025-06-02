using Application.Consultants.DTOs;
using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Persistence;

public interface IConsultantRepository
{
    Task<Result<Unit>> AddAsync(Consultant consultant, CancellationToken ct = default);
    Task<Result<Consultant>> GetByIdAsync(Guid id , CancellationToken ct = default);
    Task<Result<IEnumerable<Consultant>>> GetAllAsync(CancellationToken ct = default);
    
    // needed to be changed
    Task<Result<Consultant>> GetBySpecialityAsync(string speciality, CancellationToken ct = default);

    public Task AppendCaseAsync(Guid consultantId, Guid caseId, CancellationToken ct);


}
