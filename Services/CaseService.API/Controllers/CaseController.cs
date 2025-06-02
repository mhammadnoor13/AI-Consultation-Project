using CaseService.API.CaseService.Application.Dtos;
using CaseService.API.CaseService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CaseService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }


        [HttpPost]
        public async Task<IActionResult> SubmitCase(
            [FromBody] SubmitCaseRequest req,
            CancellationToken ct)
        {
            // call into your Application layer
            var id = await _caseService.SubmitCaseAsync(
                req.Email, req.Description, req.Speciality, ct);

            // return 201 Created with a Location header
            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
        }

        // 2) Get a single Case by Id
        // GET /api/cases/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CaseDto>> GetById(
            Guid id,
            CancellationToken ct)
        {
            var dto = await _caseService.GetCaseByIdAsync(id, ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        // 3) List all Cases in a given speciality
        // GET /api/cases/speciality/{speciality}

        [HttpGet("speciality/{speciality}")]
        public async Task<ActionResult<IEnumerable<CaseDto>>> GetBySpeciality(
            string speciality,
            CancellationToken ct)
        {
            var list = await _caseService.GetCasesBySpecialityAsync(speciality, ct);
            return Ok(list);
        }

        // 4) Transition a Case to InReview
        // POST /api/cases/{id}/inreview
        [HttpPost("{id:guid}/inreview")]
        public async Task<IActionResult> MoveToInReview(
            Guid id,
            CancellationToken ct)
        {
            await _caseService.MoveToInReviewAsync(id, ct);
            return NoContent();  // 204 No Content
        }

        // 5) Finish a Case
        // POST /api/cases/{id}/finish
        [HttpPost("{id:guid}/finish")]
        public async Task<IActionResult> FinishCase(
            Guid id,
            CancellationToken ct)
        {
            await _caseService.FinishCaseAsync(id, ct);
            return NoContent();
        }
        /*
		// 6) Delete a Case
		// DELETE /api/cases/{id}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteCase(
			Guid id,
			CancellationToken ct)
		{
			await _caseService.DeleteCaseAsync(id, ct);
			return NoContent();
		}
		*/
    }


    // These simple DTOs/request‐models live in your Api project
    public record SubmitCaseRequest(
        string Email,
        string Description,
        string Speciality
    );
}
