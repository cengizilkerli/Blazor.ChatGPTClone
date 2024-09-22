
using ChatGPTClone.Application.Features.ChatSessions.Commands.CreateRange;
using ChatGPTClone.Application.Features.ChatSessions.Commands.Remove;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers;

[Authorize]
public class ChatMessagesController : ApiControllerBase
{
    public ChatMessagesController(ISender mediatr) : base(mediatr)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChatMessageCreateCommand command, CancellationToken cancellationToken)
    => Ok(await Mediatr.Send(command, cancellationToken));

    [HttpPost("range")]
    public async Task<IActionResult> CreateRangeAsync(ChatSessionCreateRangeCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken) =>
     Ok(await Mediatr.Send(new ChatSessionRemoveCommand(id), cancellationToken));
}