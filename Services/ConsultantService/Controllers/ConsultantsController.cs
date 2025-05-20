using Application.Consultants.Commands.CreateConsultant;
using Application.Consultants.DTOs;
using Application.Consultants.Queries;
using Application.Consultants.Queries.GetAll;
using Application.Consultants.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ConsultantsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ConsultantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateConsultantDto dto)
        {
            var cmd = new CreateConsultantCmd(dto.FirstName, dto.LastName, dto.Email, dto.Speciality);
            var result = await _mediator.Send(cmd);

            return result.IsSuccess
                ? CreatedAtRoute(
                    routeName: "GetConsultantById",
                    routeValues: new { id = result.Value },
                    value: result.Value)
                : BadRequest(result.Error);
                ;

        }

        [HttpGet("{id:Guid}",Name ="GetConsultantById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetConsultantByIdQuery(id));

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllConsultantsQuery());

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }



    }
}
