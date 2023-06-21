using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Contracts;

namespace Tasks.Application.Features.Tasks.Queries
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksRequest, List<GetAllTasksResponse>>
    {
        private readonly ITasksQueryRepository _tasksQueryRepository;
        public GetAllTasksHandler(ITasksQueryRepository tasksQueryRepository)
        {
            _tasksQueryRepository = tasksQueryRepository;
        }
        public async Task<List<GetAllTasksResponse>> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
        {
            var resp = await _tasksQueryRepository.GetAll();

            List<GetAllTasksResponse> getAllTasksResponses = resp.Select(source =>
            new GetAllTasksResponse
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Description = source.Description,
                DueDate = source.DueDate,
                HaveReminder = source.HaveReminder,
                Id = source.Id,
                isActive = source.isActive,
                isPinned = source.isPinned,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                Status = source.Status,
                Title = source.Title,
                userId = source.userId,
                isCompleted = source.isCompleted
            }).ToList();

            return getAllTasksResponses;

        }
    }
}
