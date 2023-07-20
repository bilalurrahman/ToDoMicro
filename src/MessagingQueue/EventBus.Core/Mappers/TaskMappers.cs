using AutoMapper;
using EventBus.Core.Models;
using EventsBus.Messages.Events.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Mappers
{
    class TasksProfile : Profile
    {
        public TasksProfile()
        {

            CreateMap<UpdateTasksReminderDateEvent, UpdateTaskRequestModel>()
               .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
               .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
               .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
               .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
               .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
               .ForMember(dest => dest.ReminderDateTime, opt => opt.MapFrom(src => src.ReminderDateTime))
               .ForMember(dest => dest.isNotifiedForReminder, opt => opt.MapFrom(src => src.isNotifiedForReminder))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
               .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
               .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => MapFromSourceToDestinationOnConsume(src.SubTasks)))
               .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue))
               .ForMember(dest => dest.IsRepeat, opt => opt.MapFrom(src => src.IsRepeat))
               .ForMember(dest => dest.RepeatFrequency, opt => opt.MapFrom(src => src.RepeatFrequency))
               .ForMember(dest => dest.NextDueDateForRepeat, opt => opt.MapFrom(src => src.NextDueDateForRepeat));

            CreateMap<UpdateTasksDueDateEvent, UpdateTaskRequestModel>()
              .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
              .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
              .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
              .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
              .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
              .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
              .ForMember(dest => dest.ReminderDateTime, opt => opt.MapFrom(src => src.ReminderDateTime))
              .ForMember(dest => dest.isNotifiedForReminder, opt => opt.MapFrom(src => src.isNotifiedForReminder))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
              .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
              .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => MapFromSourceToDestinationOnConsume(src.SubTasks)))
              .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue))
               .ForMember(dest => dest.IsRepeat, opt => opt.MapFrom(src => src.IsRepeat))
               .ForMember(dest => dest.RepeatFrequency, opt => opt.MapFrom(src => src.RepeatFrequency))
               .ForMember(dest => dest.NextDueDateForRepeat, opt => opt.MapFrom(src => src.NextDueDateForRepeat));

           


            CreateMap<UpdateTaskNextDueDateEvent, UpdateTaskRequestModel>()
                .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.isActive))
                .ForMember(dest => dest.isPinned, opt => opt.MapFrom(src => src.isPinned))
                .ForMember(dest => dest.isCompleted, opt => opt.MapFrom(src => src.isCompleted))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
                .ForMember(dest => dest.HaveReminder, opt => opt.MapFrom(src => src.HaveReminder))
                .ForMember(dest => dest.ReminderDateTime, opt => opt.MapFrom(src => src.ReminderDateTime))
                .ForMember(dest => dest.isNotifiedForReminder, opt => opt.MapFrom(src => src.isNotifiedForReminder))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                .ForMember(dest => dest.LastModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.isDeleted, opt => opt.MapFrom(src => src.isDeleted))
                .ForMember(dest => dest.SubTasks, opt => opt.MapFrom(src => MapFromSourceToDestinationOnConsume(src.SubTasks)))
                .ForMember(dest => dest.isNotifiedForDue, opt => opt.MapFrom(src => src.isNotifiedForDue))
                .ForMember(dest => dest.IsRepeat, opt => opt.MapFrom(src => src.IsRepeat))
                .ForMember(dest => dest.RepeatFrequency, opt => opt.MapFrom(src => src.RepeatFrequency))
                .ForMember(dest => dest.NextDueDateForRepeat, opt => opt.MapFrom(src => src.NextDueDateForRepeat));

        }

        private List<Models.SubTasks> MapFromSourceToDestinationOnConsume(List<EventsBus.Messages.Events.Tasks.SubTasks> subTasks)
        {
            List<Models.SubTasks> list
                = new List<Models.SubTasks>();
            foreach (var sub in subTasks)
            {
                var destinationItem = new Models.SubTasks
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
    }
}
