using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Query;

namespace Pomodoros.Application.Features.Pomodoros.Commad.GetAll
{
    public class GetAllPomodoroHandler : IRequestHandler<GetAllPomodoroRequest, List<GetAllPomodoroResponse>>
    {
        private readonly IQueryPomodorosRepository _queryPomodorosRepository;
        private readonly IMapper _mapper;
        public GetAllPomodoroHandler(IQueryPomodorosRepository queryPomodorosRepository, IMapper mapper)
        {
            _queryPomodorosRepository = queryPomodorosRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllPomodoroResponse>> Handle(GetAllPomodoroRequest request, CancellationToken cancellationToken)
        {
            var response = await _queryPomodorosRepository.GetAll(request.TaskId);

            return _mapper.Map<List<GetAllPomodoroResponse>>(response);
        }
    }
}
