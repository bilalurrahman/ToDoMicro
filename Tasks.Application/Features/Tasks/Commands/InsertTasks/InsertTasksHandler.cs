using AutoMapper;
using EventsBus.Messages.Events.Tasks;
using MassTransit;
using MediatR;
using SharedKernal.Common.Exceptions;
using SharedKernal.Common.HttpContextHelper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Commands.InsertTasks
{
    public class InsertTasksHandler : IRequestHandler<InsertTasksRequest, InsertTasksResponse>
    {

        private readonly IHttpContextHelper _httpContextHelper;
        private readonly ITasksCommandsRepository _tasksCommandsRepository;
        private readonly IBus _ibus;
        private readonly IMapper _mapper;
        public InsertTasksHandler(ITasksCommandsRepository tasksCommandsRepository,
            IHttpContextHelper httpContextHelper, IBus ibus, IMapper mapper)
        {
            _tasksCommandsRepository = tasksCommandsRepository;
            _httpContextHelper = httpContextHelper;
            _ibus = ibus;
            _mapper = mapper;
        }
        public async Task<InsertTasksResponse> Handle(InsertTasksRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextHelper.CurrentLoggedInId;
            if(string.IsNullOrEmpty(request?.Title))
                throw new BusinessRuleException(LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Id, LogEventIds.BusinessRuleEventIds.TitleCantBeEmpty.Name);



            //if havereminder is set and due date is defined
            //create the job
            request.CreatedBy = userId;
            request.LastModifiedBy = userId;
            request.LastModifiedDate = DateTime.Now;
            request.CreatedDate = DateTime.Now;
            

            var createTaskRepoRequest = _mapper.Map<TasksEntity>(request);
            createTaskRepoRequest.userId = Convert.ToInt64(userId);
            var resp =await _tasksCommandsRepository.CreateTask(createTaskRepoRequest);
            await PublishEvent(request, userId);

            return new InsertTasksResponse { Id = resp};


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
