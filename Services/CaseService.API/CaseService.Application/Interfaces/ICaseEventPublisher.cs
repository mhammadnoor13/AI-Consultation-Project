namespace CaseService.API.CaseService.Application.Interfaces
{
    public interface ICaseEventPublisher
    {
        Task PublishCaseCreatedAsync(Guid caseId, string speciality, CancellationToken ct);
        Task PublishCaseInReviewAsync(Guid caseId, CancellationToken ct);
        Task PublishCaseFinishedAsync(Guid caseId, CancellationToken ct);
    }
}

