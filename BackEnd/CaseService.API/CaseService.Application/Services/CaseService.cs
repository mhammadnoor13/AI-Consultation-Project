using CaseService.API.CaseService.Application.Dtos;
using CaseService.API.CaseService.Application.Interfaces;
using CaseService.API.CaseService.Domain.Entities;

namespace CaseService.API.CaseService.Application.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _repo;
        private readonly ICaseEventPublisher _events;

        public CaseService(
            ICaseRepository repo,
            ICaseEventPublisher events)
        {
            _repo = repo;
            _events = events;
        }

        public async Task<Guid> SubmitCaseAsync(
            string email,
            string description,
            string speciality,
            CancellationToken ct)
        {
            // 1) Create the domain entity (validates inputs)
            var c = Case.Create(email, description, speciality);

            // 2) Persist it via the repository
            await _repo.SaveAsync(c, ct);

            // 3) Publish the “CaseCreated” integration event
            await _events.PublishCaseCreatedAsync(c.Id, c.Email, ct);

            return c.Id;
        }

        public async Task MoveToInReviewAsync(Guid id, CancellationToken ct)
        {
            // 1) Load the case
            var c = await _repo.GetByIdAsync(id, ct)
                   ?? throw new KeyNotFoundException($"Case {id} not found");

            // 2) Apply domain behavior
            c.MoveToInReview();

            // 3) Persist new status
            await _repo.SaveAsync(c, ct);

            // 4) Publish the “InReview” integration event
            await _events.PublishCaseInReviewAsync(id, ct);
        }

        public async Task FinishCaseAsync(Guid id, CancellationToken ct)
        {
            // 1) Load the case
            var c = await _repo.GetByIdAsync(id, ct)
                   ?? throw new KeyNotFoundException($"Case {id} not found");

            // 2) Apply domain behavior
            c.Finish();

            // 3) Persist new status
            await _repo.SaveAsync(c, ct);

            // 4) Publish the “Finished” integration event
            await _events.PublishCaseFinishedAsync(id, ct);
        }

        public async Task<CaseDto?> GetCaseByIdAsync(Guid id, CancellationToken ct)
        {
            var c = await _repo.GetByIdAsync(id, ct);
            if (c is null) return null;
            return ToDto(c);
        }

        private static CaseDto ToDto(Case c)
            => new CaseDto(
                c.Id,
                c.Email,
                c.Description,
                c.Speciality,
                c.Status,
                c.CreatedAt);

        public async Task<IEnumerable<CaseDto>> GetCasesBySpecialityAsync(string speciality, CancellationToken ct)
        {
            // 1) Ask the repository for all matching Case entities
            var cases = await _repo.GetBySpecialityAsync(speciality, ct);

            // 2) Map each to a DTO and return
            return cases.Select(c => new CaseDto(
                c.Id,
                c.Email,
                c.Description,
                c.Speciality,
                c.Status,
                c.CreatedAt
            ));
        }
    }
}
