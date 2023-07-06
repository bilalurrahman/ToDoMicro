using System;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Contracts.Persistance;
using Authentication.Domain.Entities;
using AutoMapper;
using MediatR;
namespace Authentication.Application.Features.Token.Command.UpdateRefreshToken
{
    public class UpdateRefreshTokenHandler : IRequestHandler<UpdateRefreshTokenRequest, UpdateRefreshTokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserCommandRepository _userCommandRepository;
        public UpdateRefreshTokenHandler(IUserCommandRepository userCommandRepository, IMapper mapper)
        {
            _userCommandRepository = userCommandRepository;
            _mapper = mapper;
        }
        public async Task<UpdateRefreshTokenResponse> Handle(UpdateRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var RefreshTokenRequest = _mapper.Map<UserToken>(request);

            return new UpdateRefreshTokenResponse
            {
                isSuccess = await _userCommandRepository.UpdateRefreshToken(RefreshTokenRequest)
            };
        }
    }
}
