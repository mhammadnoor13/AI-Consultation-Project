using ConsultantService.ConsultantService.Application.Interfaces;
using ConsultantService.ConsultantService.Domain.Entities;
using ConsultantService.ConsultantService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultantService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConsultantsController : ControllerBase
    {
        private readonly IConsultantService _repository;
        private readonly IConsultantAssignmentPolicy _assignmentPolicy;

        public ConsultantsController(
            IConsultantService repository,
            IConsultantAssignmentPolicy assignmentPolicy)
        {
            _repository = repository;
            _assignmentPolicy = assignmentPolicy;
        }

        [HttpGet("{id:guid}/specialty")]
        public async Task<ActionResult<string>> GetSpecialty(Guid id)
        {
            var consultant = await _repository.GetByIdAsync(id);
            if (consultant == null)
                return NotFound();

            return Ok(consultant.Specialty);
        }


        public class AssignCaseRequest
        {
            public Guid CaseId { get; set; }
            public string Specialty { get; set; }
        }

        [HttpPost("assign-case")]
        public async Task<ActionResult<Guid>> AssignCase([FromBody] AssignCaseRequest req)
        {
            // 1) load all candidates with that specialty
            var candidates = await _repository
                .ListBySpecialtyAsync(req.Specialty, onlyActive: true);

            // 2) pick one by your domain policy
            var chosen = _assignmentPolicy.SelectCandidate(candidates);

            if (chosen == null)
                return Conflict("No consultant available for that specialty.");



            return Ok(chosen.Id);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateConsultantRequest req)
        {
            // 1) Use the domain factory to build a valid Consultant
            var consultant = Consultant.Create(
                req.FirstName,
                req.LastName,
                req.Specialty,
                req.Email);

            // 2) Persist via the application service
            await _repository.AddAsync(consultant);

            // 3) Return 201 Created with Location header
            return CreatedAtAction(
                nameof(GetSpecialty),
                new { id = consultant.Id },
                consultant.Id);
        }
    }
    
}
