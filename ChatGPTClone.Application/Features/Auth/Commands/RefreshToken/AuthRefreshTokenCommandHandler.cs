using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using MediatR;

namespace ChatGPTClone.Application.Features.Auth.Commands.RefreshToken
{
    public class AuthRefreshTokenCommandHandler : IRequestHandler<AuthRefreshTokenCommand, ResponseDto<AuthRefreshTokenResponse>>
    {
        public Task<ResponseDto<AuthRefreshTokenResponse>> Handle(AuthRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}