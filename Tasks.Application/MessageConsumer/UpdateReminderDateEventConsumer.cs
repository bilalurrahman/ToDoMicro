using EventsBus.Messages.Events.Tasks;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tasks.Domain.Entities;
using Tasks.Application.Features.Tasks.Commands.UpdateTask;

namespace Tasks.Application.MessageConsumer
{
    public class UpdateReminderDateEventConsumer : IConsumer<UpdateTasksReminderDateEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReminderDateEventConsumer> _logger;
        public UpdateReminderDateEventConsumer(IMediator mediator, IMapper mapper, ILogger<UpdateReminderDateEventConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UpdateTasksReminderDateEvent> context)
        {
            var updateTaskRepoRequest = _mapper.Map<UpdateTaskRequest>(context?.Message);
            var resp = await _mediator.Send(updateTaskRepoRequest);
            _logger.LogInformation("Reminder Date Updated Successfully ");
        }
    }
}
