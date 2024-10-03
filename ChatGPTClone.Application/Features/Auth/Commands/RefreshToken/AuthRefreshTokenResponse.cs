using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatGPTClone.Application.Features.Auth.Commands.RefreshToken
{
    public sealed class AuthRefreshTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }

        public AuthRefreshTokenResponse()
        {
            
        }

        public AuthRefreshTokenResponse(string token, DateTime expireAt)
        {
            Token = token;
            ExpireAt = expireAt;
        }
    }
}