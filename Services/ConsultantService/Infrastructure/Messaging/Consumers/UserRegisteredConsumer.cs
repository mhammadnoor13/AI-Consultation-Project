using MassTransit;
using Contracts;
using MediatR;
using ConsultantService.Application.Consultants.Commands.CreateConsultantFromEvent;
using System.Text.Json;

public class UserRegisteredConsumer : IConsumer<IUserRegistered>
{
    private readonly IMediator _mediator;

    public UserRegisteredConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserRegistered> context)
    {
        var msg = context.Message;
        var command = new CreateConsultantFromEventCommand(
            msg.Id,
            msg.Email,
            msg.FirstName,
            msg.LastName,
            msg.Speciality
        );

        await _mediator.Send(command);
    }
}
