using Authentication.Application.Features.Register.Queries.GetUser;
using Authentication.Application.Features.Token.Command.UpdateRefreshToken;
using Authentication.Common.Extensions;
using Authentication.Common.Helpers.JWTHelper;
using MediatR;
using SharedKernal.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
       private readonly IJWTCreateToken _iJWTCreateToken;
        private readonly IMediator _mediator;

        public LoginHandler(IJWTCreateToken iJWTCreateToken, IMediator mediator)
        {
            _iJWTCreateToken = iJWTCreateToken;
            _mediator = mediator;
        }
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserRequest
            {
                username = request.username
            });
            if (user?.Id > 0)
            {
                var isCorrectPassword = Extensions.VerifyHashedValues(request.password, user.password);
                if (isCorrectPassword)
                {
                    var Response = await _iJWTCreateToken.GenerateToken(request.username, user.Id);
                    //Save the token here
                    var updateTokenResponse = await _mediator.Send(new UpdateRefreshTokenRequest {refresh_token=Response.RefreshToken,refresh_token_expiry=Response.RefreshTokenExpiry,Username=request.username});
                    if (updateTokenResponse == null || !updateTokenResponse.isSuccess)
                        throw new CommonException("Token Update Error");
                    return new LoginResponse
                    {
                        Token = Response.Token,
                        RefreshToken=Response.RefreshToken,
                        RefreshTokenExpiry = Response.RefreshTokenExpiry
                    };
                }
            }
            else
            {
                throw new EntityNotFoundException(LogEventIds.EntityNotFoundEventIds.IncorrectUserName.Id, LogEventIds.EntityNotFoundEventIds.IncorrectUserName.Name);
            }

            return new LoginResponse();
        }
    }
}
