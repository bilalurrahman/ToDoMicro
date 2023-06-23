using AutoMapper;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.InsertTasks
{
    public class InsertTasksHandler : IRequestHandler<InsertTasksRequest, InsertTasksResponse>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IBus _ibus;
        private readonly IMapper _mapper;
        public InsertTasksHandler(ITasksCommandsRepository tasksCommandsRepository,
            IHttpContextAccessor httpContextAccessor, IBus ibus, IMapper mapper)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            _httpContextAccessor = httpContextAccessor;
            _ibus = ibus;
            _mapper = mapper;
        }
        public async Task<InsertTasksResponse> Handle(InsertTasksRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;

            var createTaskRepoRequest = _mapper.Map<TasksEntity>(request);
            createTaskRepoRequest.userId = Convert.ToInt64(userId);
            await _tasksCommandsRepository.CreateTask(createTaskRepoRequest);
            await PublishEvent(request, userId);

            return new InsertTasksResponse();


        }

        private async Task PublishEvent(InsertTasksRequest request, string userId)
        {
            var eventMessage = new NewTaskEmailCreationEvent
            {
                userDetails = new userDetails
                {
                    userId = int.Parse(userId),
                },
                dueDate = request.DueDate,
                description = request.Description,
                title = request.Title
            };
            await _ibus.Publish(eventMessage);
            //var eventPublisher = new EventPublisher<NewTaskEmailCreationEvent>(_ibus);
            //await eventPublisher.Publish(eventMessage);
        }
    }
}
