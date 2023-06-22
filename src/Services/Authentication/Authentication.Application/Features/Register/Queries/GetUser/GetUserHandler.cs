using Authentication.Application.Contracts.Persistance;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Register.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _imapper;
        public GetUserHandler(IUserQueryRepository userQueryRepository, IMapper imapper)
        {
            _userQueryRepository = userQueryRepository;
            _imapper = imapper;
        }
        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.GetUserInfo(request.username);
            if (user==null || user?.Id==0)
            {
                return new GetUserResponse();
            }

            return _imapper.Map<GetUserResponse>(user);            
        }
    }
}
