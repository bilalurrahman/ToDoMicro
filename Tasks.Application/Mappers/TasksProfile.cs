using AutoMapper;
using EventsBus.Messages.Events.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Application.Features.Tasks.Commands.InsertTasks;
using Tasks.Application.Features.Tasks.Commands.UpdateTask;
using Tasks.Application.Features.Tasks.Queries;
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

            CreateMap<TasksEntity, GetAllTasksResponse>()
            .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
            .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
            .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
            .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
            .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => src.SubTasks))
            .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue));

            CreateMap<TasksEntity, UpdateTasksDueDateEvent>()
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
                .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
                .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => MapFromSourceToDestinationOnPublish(src.SubTasks)))
                .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue));

          
            CreateMap<UpdateTasksDueDateEvent, UpdateTaskRequest>()
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
                .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
                .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => MapFromSourceToDestinationOnConsume(src.SubTasks)))                
                .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue));

        }

        private List<Domain.Entities.SubTasks> MapFromSourceToDestinationOnConsume(List<EventsBus.Messages.Events.Tasks.SubTasks> subTasks)
        {
            List<Domain.Entities.SubTasks> list
                = new List<Domain.Entities.SubTasks>();
            foreach (var sub in subTasks)
            {
                var destinationItem = new Domain.Entities.SubTasks
                {
                    CreatedBy = sub.CreatedBy,
                    CreatedDate = sub.CreatedDate,
                    Description = sub.Description,
                    Id = sub.Id,
                    isActive = sub.isActive,
                    isCompleted = sub.isCompleted,
                    isDeleted = sub.isDeleted,
                    LastModifiedBy = sub.LastModifiedBy,
                    LastModifiedDate = sub.LastModifiedDate,
                    userId = sub.userId
                };
                list.Add(destinationItem);
            }

            return list;
        }

        private List<EventsBus.Messages.Events.Tasks.SubTasks> MapFromSourceToDestinationOnPublish(List<Domain.Entities.SubTasks> subTasks)
        {
            List<EventsBus.Messages.Events.Tasks.SubTasks> list 
                = new List<EventsBus.Messages.Events.Tasks.SubTasks>();
            foreach(var sub in subTasks)
            {
                var destinationItem = new EventsBus.Messages.Events.Tasks.SubTasks
                {
                    CreatedBy = sub.CreatedBy,
                    CreatedDate = sub.CreatedDate,
                    Description =sub.Description,
                    Id = sub.Id,
                    isActive = sub.isActive,
                    isCompleted=sub.isCompleted,
                    isDeleted = sub.isDeleted,
                    LastModifiedBy = sub.LastModifiedBy,
                    LastModifiedDate = sub.LastModifiedDate,
                    userId = sub.userId
                };
                list.Add(destinationItem);
            }

            return list;
        }
    }
}
