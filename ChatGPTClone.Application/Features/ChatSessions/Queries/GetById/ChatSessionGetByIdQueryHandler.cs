using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessionGetByIdQuery, ChatSessionGetByIdDto>
{
    private readonly IChatSessionCacheService _chatSessionCacheService;

    public ChatSessionGetByIdQueryHandler(IChatSessionCacheService chatSessionCacheService)
    {
        _chatSessionCacheService = chatSessionCacheService;
    }

    public async Task<ChatSessionGetByIdDto> Handle(ChatSessionGetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _chatSessionCacheService.GetByIdAsync(request.Id, cancellationToken);
    }
}
