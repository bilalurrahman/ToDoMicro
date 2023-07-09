using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.Persistance;
using Authentication.Application.Features.Token.Command.UpdateRefreshToken;
using Authentication.Common.Helpers.JWTHelper;
using MediatR;
using SharedKernal.Common.Exceptions;

namespace Authentication.Application.Features.Client.Login
{
    public class ClientLoginHandler : IRequestHandler<ClientLoginRequest, ClientLoginResponse>
    {
        private readonly IUserQueryRepository _userQueryRepo;
        private readonly IJWTCreateToken _iJWTCreateToken;
        private readonly IMediator _mediator;

        public ClientLoginHandler(IUserQueryRepository userQueryRepo, 
            IMediator mediator, IJWTCreateToken iJWTCreateToken)
        {
            _userQueryRepo = userQueryRepo;
            _mediator = mediator;
            _iJWTCreateToken = iJWTCreateToken;
        }

        public async Task<ClientLoginResponse> Handle(ClientLoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _userQueryRepo.GetClientLogin(new Domain.Entities.Client
            {
                Username = request.ClientUsername,
                Password = request.ClientPassword
            });
            if (response > 0)
            {
                var Response = await _iJWTCreateToken.GenerateToken(request.ClientUsername, response);
                //Save the token here
                var updateTokenResponse = await _mediator.Send(new UpdateRefreshTokenRequest { refresh_token = Response.RefreshToken, 
                    refresh_token_expiry = Response.RefreshTokenExpiry, Username = request.ClientUsername });
                if (updateTokenResponse == null || !updateTokenResponse.isSuccess)
                    throw new CommonException("Token Update Error");
                return new ClientLoginResponse
                {
                    Token = Response.Token,
                    RefreshToken = Response.RefreshToken,
                    RefreshTokenExpiry = Response.RefreshTokenExpiry
                };
            }
            else
                throw new EntityNotFoundException(LogEventIds.EntityNotFoundEventIds.IncorrectUserName.Id, 
                    LogEventIds.EntityNotFoundEventIds.IncorrectUserName.Name);
        }
    }
}
