using ConsultantService.ConsultantService.Domain.Entities;

namespace ConsultantService.ConsultantService.Application.Interfaces
{
    public interface IConsultantRepository
    {
        /// <summary>
        /// Add a new consultant (e.g. on registration).
        /// </summary>
        Task AddAsync(Consultant consultant);

        /// <summary>
        /// Update an existing consultant (e.g. after assign/release).
        /// </summary>
        Task UpdateAsync(Consultant consultant);

        /// <summary>
        /// Retrieve a consultant by its unique identifier.
        /// </summary>
        Task<Consultant?> GetByIdAsync(Guid id);

        /// <summary>
        /// List all consultants matching the given specialty.
        /// </summary>
        Task<IEnumerable<Consultant>> ListBySpecialtyAsync(
            string specialty,
            bool onlyActive = true);
    }
}
