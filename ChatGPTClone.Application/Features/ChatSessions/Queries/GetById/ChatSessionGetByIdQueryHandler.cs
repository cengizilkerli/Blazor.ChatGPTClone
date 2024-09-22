using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessionGetByIdQuery, ChatSessionGetByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private const string _cacheKey = "ChatSessionGetAll_";
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public ChatSessionGetByIdQueryHandler(IApplicationDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
        .SetPriority(CacheItemPriority.High);
    }

    public async Task<ChatSessionGetByIdDto> Handle(ChatSessionGetByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{_cacheKey}{request.Id}";

        if (_cache.TryGetValue(cacheKey, out ChatSessionGetByIdDto? cachedResult))
            return cachedResult;

        var chatSession = await _context.ChatSessions
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var chatSessionGetById = ChatSessionGetByIdDto.MapFromChatSession(chatSession);

        _cache.Set(cacheKey, chatSessionGetById, _cacheOptions);

        return chatSessionGetById;
    }
}
