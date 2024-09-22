using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;

public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserServices _currentUserService;

    private readonly IMemoryCache _cache;
    private const string _cacheKey = "ChatSessionGetAll_";
    private readonly MemoryCacheEntryOptions _cacheOptions;

    public ChatSessionGetAllQueryHandler(IApplicationDbContext context, ICurrentUserServices currentUserService, IMemoryCache cache)
    {
        _context = context;
        _currentUserService = currentUserService;
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
        .SetPriority(CacheItemPriority.High);
    }

    public async Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"{_cacheKey}{_currentUserService.UserId}";

        if (_cache.TryGetValue(cacheKey, out List<ChatSessionGetAllDto>? cachedResult))
            return cachedResult;

        var chatSessions = await _context
            .ChatSessions
            .AsNoTracking()
            .Where(x => x.AppUserId == _currentUserService.UserId)
            .OrderByDescending(cs => cs.CreatedOn)
            .Select(x => ChatSessionGetAllDto.MapFromChatSession(x))
            .ToListAsync(cancellationToken);

        _cache.Set(cacheKey, chatSessions, _cacheOptions);

        return chatSessions;
    }
}