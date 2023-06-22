using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Features.Tasks.Commands.InsertTasks;
using Tasks.Application.Features.Tasks.Commands.UpdateTask;
using Tasks.Application.Features.Tasks.Queries.GetTask;
using Tasks.Domain.Entities;

namespace Tasks.Application.Mappers
{
    public class TasksProfile:Profile
    {
        public TasksProfile()
        {
            CreateMap<TasksEntity, GetTaskResponse>().ReverseMap();
            CreateMap<InsertTasksRequest, TasksEntity>().ReverseMap();
            CreateMap<UpdateTaskRequest, TasksEntity>().ReverseMap();
        }
    }
}
