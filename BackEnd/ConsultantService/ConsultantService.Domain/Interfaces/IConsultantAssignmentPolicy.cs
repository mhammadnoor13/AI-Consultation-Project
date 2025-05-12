using ConsultantService.ConsultantService.Domain.Entities;

namespace ConsultantService.ConsultantService.Domain.Interfaces
{
    public interface IConsultantAssignmentPolicy
    {
        Consultant SelectCandidate(IEnumerable<Consultant> consultants);
    }
}
