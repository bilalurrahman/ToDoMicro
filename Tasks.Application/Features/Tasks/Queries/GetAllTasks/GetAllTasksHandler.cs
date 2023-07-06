using AutoMapper;
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
        private readonly IMapper _mapper;
        public GetAllTasksHandler(ITasksQueryRepository tasksQueryRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _tasksQueryRepository = tasksQueryRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<List<GetAllTasksResponse>> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value;
            var resp = await _tasksQueryRepository.GetAll(long.Parse(userId));
            var filteredResponse = FilteredTasks(resp);

            return _mapper.Map<List<GetAllTasksResponse>>(filteredResponse);

        }

        private static List<TasksEntity> FilteredTasks(List<TasksEntity> resp)
        {

            //Business to get all the tasks that are not deleted
            return resp.Select(r =>
            {
                r.SubTasks = r?.SubTasks?.Where(x => !x.isDeleted).ToList();
                return r;
            }).Where(x => !x.isDeleted).ToList();

            
        }
    }
}
