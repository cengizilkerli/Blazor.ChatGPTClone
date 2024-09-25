using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;

public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
{
   
    private readonly IChatSessionCacheService _chatSessionCacheService;

    public ChatSessionGetAllQueryHandler(IChatSessionCacheService chatSessionCacheService)
    {
        _chatSessionCacheService = chatSessionCacheService;
    }

    public async Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
    {
        return await _chatSessionCacheService.GetAllAsync(cancellationToken);
    }
}