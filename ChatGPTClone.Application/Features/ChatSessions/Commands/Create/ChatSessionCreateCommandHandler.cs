using ChatGPTClone.Application.Common.Interfaces;
using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create;

public class ChatSessionCreateCommandHandler : IRequestHandler<ChatSessionCreateCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserServices _currentUserServices;

    public ChatSessionCreateCommandHandler(IApplicationDbContext context, ICurrentUserServices currentUserServices)
    {
        _context = context;
        _currentUserServices = currentUserServices;
    }

    public async Task<Guid> Handle(ChatSessionCreateCommand request, CancellationToken cancellationToken)
    {
        var chatSession = request.ToChatSession(_currentUserServices.UserId);

        _context.ChatSessions.Add(chatSession);
        await _context.SaveChangesAsync(cancellationToken);

        return chatSession.Id;
    }
}