using CaseService.API.CaseService.Domain.Entities;

namespace CaseService.API.CaseService.Application.Interfaces
{
    public interface ICaseRepository
    {
        Task SaveAsync(Case c, CancellationToken ct);
        Task<Case?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Case>> GetBySpecialityAsync(string speciality, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
