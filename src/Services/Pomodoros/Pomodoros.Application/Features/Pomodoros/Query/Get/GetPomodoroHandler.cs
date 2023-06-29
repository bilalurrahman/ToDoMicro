using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Pomodoros.Application.Contracts.Persistance.Query;

namespace Pomodoros.Application.Features.Pomodoros.Commad.Get
{
    public class GetPomodoroHandler : IRequestHandler<GetPomodoroRequest, GetPomodoroResponse>
    {
        private readonly IQueryPomodorosRepository _queryPomodorosRepository;
        private readonly IMapper _mapper;
        public GetPomodoroHandler(IMapper mapper, IQueryPomodorosRepository queryPomodorosRepository)
        {
            _mapper = mapper;
            _queryPomodorosRepository = queryPomodorosRepository;
        }
        public  async Task<GetPomodoroResponse> Handle(GetPomodoroRequest request, CancellationToken cancellationToken)
        {
            var response = await _queryPomodorosRepository.Get(request.Id);
            return _mapper.Map<GetPomodoroResponse>(response);
        }
    }
}
