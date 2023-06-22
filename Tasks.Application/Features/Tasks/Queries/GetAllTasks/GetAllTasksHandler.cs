using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Application.Contracts;
using Tasks.Domain.Entities;

namespace Tasks.Application.Features.Tasks.Queries
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksRequest, List<GetAllTasksResponse>>
    {
        private readonly ITasksQueryRepository _tasksQueryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetAllTasksHandler(ITasksQueryRepository tasksQueryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _tasksQueryRepository = tasksQueryRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<GetAllTasksResponse>> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;
            var resp = await _tasksQueryRepository.GetAll(long.Parse(userId));
            List<GetAllTasksResponse> getAllTasksResponses = Mapper(resp);
            return getAllTasksResponses;

        }

        private static List<GetAllTasksResponse> Mapper(List<TasksEntity> resp)
        {
            return resp.Select(source =>
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
        }
    }
}
