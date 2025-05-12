using ConsultantService.ConsultantService.Application.Interfaces;
using ConsultantService.ConsultantService.Domain.Entities;
using ConsultantService.ConsultantService.Domain.Interfaces;

namespace ConsultantService.ConsultantService.Application.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly IConsultantRepository _repository;
        private readonly IConsultantAssignmentPolicy _assignmentPolicy;

        public ConsultantService(
            IConsultantRepository repository,
            IConsultantAssignmentPolicy assignmentPolicy)
        {
            _repository = repository;
            _assignmentPolicy = assignmentPolicy;
        }

        public Task AddAsync(Consultant consultant)
        {
            // Additional application-level validation or orchestration can go here
            return _repository.AddAsync(consultant);
        }

        public Task UpdateAsync(Consultant consultant)
        {
            return _repository.UpdateAsync(consultant);
        }

        public Task<Consultant> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<Consultant>> ListBySpecialtyAsync(string specialty, bool onlyActive = true)
        {
            return _repository.ListBySpecialtyAsync(specialty);
        }

        // Example of using assignment policy within service
        public async Task<Guid> AssignCaseAsync(Guid caseId, string specialty)
        {
            var candidates = await _repository.ListBySpecialtyAsync(specialty);
            var chosen = _assignmentPolicy.SelectCandidate(candidates);
            if (chosen == null)
                throw new InvalidOperationException("No consultant available for that specialty.");

            await _repository.UpdateAsync(chosen);
            return chosen.Id;
        }
    }
}
