using Authentication.Application.Contracts.Persistance;
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
        public GetUserHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }
        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userQueryRepository.Get(request.username);
            if (user?.Id==0)
            {
                return new GetUserResponse();
            }
            return new GetUserResponse
            {
                Id = user.Id,
                username = user?.Username,
                password = user?.Password,
                isActive = (bool)(user?.isActive)
            };
        }
    }
}
