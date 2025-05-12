using CaseService.API.CaseService.Application.Interfaces;

namespace CaseService.API.CaseService.Infrastructure.Messaging
{
    public class NoOpCaseEventPublisher : ICaseEventPublisher
    {
        public Task PublishCaseCreatedAsync(Guid caseId, string email, CancellationToken ct)
            => Task.CompletedTask;
        public Task PublishCaseInReviewAsync(Guid caseId, CancellationToken ct)
            => Task.CompletedTask;
        public Task PublishCaseFinishedAsync(Guid caseId, CancellationToken ct)
            => Task.CompletedTask;
    }
}
