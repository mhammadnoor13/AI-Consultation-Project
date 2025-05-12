using CaseService.API.CaseService.Application.Dtos;

namespace CaseService.API.CaseService.Application.Interfaces
{
    public interface ICaseService
    {
        // This interface declares all the use-cases the service supports
        Task<Guid> SubmitCaseAsync(string email, string description, string speciality, CancellationToken ct);
        Task MoveToInReviewAsync(Guid id, CancellationToken ct);
        Task FinishCaseAsync(Guid id, CancellationToken ct);
        Task<CaseDto?> GetCaseByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<CaseDto>> GetCasesBySpecialityAsync(String speciality, CancellationToken ct);

    }
}
