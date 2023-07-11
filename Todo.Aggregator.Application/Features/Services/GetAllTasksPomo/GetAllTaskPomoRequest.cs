using MediatR;
using System.Collections.Generic;

namespace Todo.Aggregator.Application.Features.Services.GetAllTasksPomo
{
   public class GetAllTaskPomoRequest:
        IRequest<List<GetAllTaskPomoResponse>>
    {
        
    }
}
