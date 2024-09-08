using ChatGPTClone.Application.Common.Models.General;
using MediatR;

namespace ChatGPTClone.Application.Features.ReSendEmailVerificationEmail;

public class AuthReSendEmailVerificationEmailCommand : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }

    public AuthReSendEmailVerificationEmailCommand(string email)
    {
        Email = email;
    }
}