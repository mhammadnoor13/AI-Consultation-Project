using ConsultantService.Application.Consultants.Commands.AssignCase;
using ConsultantService.Application.Consultants.Commands.CreateConsultantFromEvent;
using Contracts;
using MassTransit;
using MediatR;
using Serilog;

namespace ConsultantService.Infrastructure.Messaging.Consumers
{
    public class CaseSubmittedConsumers : IConsumer<ICaseSubmitted>
    {
        private readonly IMediator _mediator;

        public CaseSubmittedConsumers(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ICaseSubmitted> context)
        {
            Log.Information("[CONSUME] got {CaseId} – {Spec}", context.Message.Id, context.Message.Speciality);

            var ev = context.Message;
            
            await _mediator.Send(new AssignCaseCommand(ev.Id, ev.Speciality), context.CancellationToken);

        }
    }
}
