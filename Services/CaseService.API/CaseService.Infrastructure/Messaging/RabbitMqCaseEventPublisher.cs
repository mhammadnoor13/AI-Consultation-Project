using CaseService.API.CaseService.Application.Interfaces;
using MassTransit;
using Contracts;

namespace CaseService.API.CaseService.Infrastructure.Messaging
{
    public class RabbitMqCaseEventPublisher : ICaseEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public async Task PublishCaseCreatedAsync(Guid caseId, string speciality, CancellationToken ct)
        {
            await _publishEndpoint.Publish<ICaseSubmitted>(new
            {
                Id = caseId, Speciality = speciality
            });
        }

        public Task PublishCaseFinishedAsync(Guid caseId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task PublishCaseInReviewAsync(Guid caseId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
