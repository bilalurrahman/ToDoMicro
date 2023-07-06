using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Features.Token.Query.GetRefreshToken;
using Authentication.Common.Helpers.JWTHelper;
using AutoMapper;
using MediatR;
using SharedKernal.Common.Exceptions;

namespace Authentication.Application.Features.Token
{
    public class VerifyRefreshTokenHandler : IRequestHandler<VerifyRefreshTokenRequest, VerifyRefreshTokenResponse>
    {
        private readonly IJWTCreateToken _iJWTCreateToken;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public VerifyRefreshTokenHandler(IMediator mediator, IJWTCreateToken iJWTCreateToken, IMapper mapper)
        {
            _mediator = mediator;
            _iJWTCreateToken = iJWTCreateToken;
            _mapper = mapper;
        }
        public async Task<VerifyRefreshTokenResponse> Handle(VerifyRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var principal = _iJWTCreateToken.GetPrincipalFromExpiredToken(request.Token);
            var username = principal.Identity.Name;
            var userid = int.Parse(principal.FindFirst("UserId").Value);
            var response = await _mediator.Send(new GetRefreshTokenRequest { username = username });
            if (response is null || response.refresh_token != request.RefreshToken || response.refresh_token_expiry <= DateTime.Now)
                throw new CommonException("Token is invalid or Expired");

            var newToken = await _iJWTCreateToken.GenerateToken(username, userid);

            return _mapper.Map<VerifyRefreshTokenResponse>(newToken);
            
        }
    }
}
