using ConsultantService.ConsultantService.Domain.Entities;

namespace ConsultantService.ConsultantService.Application.Interfaces
{
    public interface IConsultantService
    {
        Task AddAsync(Consultant consultant);
        Task UpdateAsync(Consultant consultant);
        Task<Consultant> GetByIdAsync(Guid id);
        Task<IEnumerable<Consultant>> ListBySpecialtyAsync(string specialty,bool onlyActive = true);



    }
}
