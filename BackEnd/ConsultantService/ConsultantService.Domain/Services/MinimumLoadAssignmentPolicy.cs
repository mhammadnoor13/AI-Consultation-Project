using ConsultantService.ConsultantService.Domain.Entities;
using ConsultantService.ConsultantService.Domain.Interfaces;
using Microsoft.AspNetCore.Routing.Matching;

namespace ConsultantService.ConsultantService.Domain.Services
{
    public class MinimumLoadAssignmentPolicy : IConsultantAssignmentPolicy
    {
        public Consultant SelectCandidate(IEnumerable<Consultant> consultants)
        {
            return consultants.OrderBy(c => c.CurrentLoad).FirstOrDefault();

        }
    }
}
