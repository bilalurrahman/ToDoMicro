//using Authentication.Common.Helpers.JWTHelper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.Application.Features.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
     //   private readonly IJWTCreateToken _iJWTCreateToken;
        public LoginHandler()
        {
            //_iJWTCreateToken = iJWTCreateToken;
        }
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {

            if(request.username=="admin" && request.password == "123")
            {
               // var Response = await _iJWTCreateToken.Generate(request.username);
                //return new LoginResponse
                //{
                //    Token = Response.Token,
                //    Expiry = Response.Expiry
                //};
                return new LoginResponse
                {
                    Token = ""
                };
            }

            return new LoginResponse();
        }
    }
}
