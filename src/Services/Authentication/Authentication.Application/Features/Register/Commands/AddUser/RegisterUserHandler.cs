using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using MediatR;
using Authentication.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Features.Register.Queries.GetUser;

namespace Authentication.Application.Features.Register.Commands.AddUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IMediator _mediator;
        public RegisterUserHandler(IUserCommandRepository userCommandRepository,
            IMediator mediator)
        {
            _userCommandRepository = userCommandRepository;
            _mediator = mediator;
        }
        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserRequest
            {
                username = request.Username
            }); 
            if(user!=null && user.Id > 0)
            {
                return new RegisterUserResponse();
            }
            return new RegisterUserResponse
            {
                isRegistered = await _userCommandRepository.Insert(new RegisterUser
                {
                    Username = request.Username,
                    Password = Extensions.HashValues(request.Password)
                })
            };
            
        }
    }
}
