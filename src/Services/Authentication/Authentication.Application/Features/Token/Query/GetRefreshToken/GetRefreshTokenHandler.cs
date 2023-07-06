using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.Persistance;
using AutoMapper;
using MediatR;
namespace Authentication.Application.Features.Token.Query.GetRefreshToken
{
    public class GetRefreshTokenHandler : IRequestHandler<GetRefreshTokenRequest, GetRefreshTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserQueryRepository _userQueryRepository;
        public GetRefreshTokenHandler(IUserQueryRepository userQueryRepository, IMapper mapper)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }
        public async Task<GetRefreshTokenResponse> Handle(GetRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var response = await _userQueryRepository.GetRefreshToken(request.username);

            return _mapper.Map<GetRefreshTokenResponse>(response);
        }
    }
}
