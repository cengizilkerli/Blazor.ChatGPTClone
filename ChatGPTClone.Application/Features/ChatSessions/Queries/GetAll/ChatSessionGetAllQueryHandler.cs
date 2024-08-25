using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;

public class ChatSessionGetAllQueryHandler : IRequestHandler<ChatSessionGetAllQuery, List<ChatSessionGetAllDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserServices _currentUserService;

    public ChatSessionGetAllQueryHandler(IApplicationDbContext context, ICurrentUserServices currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public Task<List<ChatSessionGetAllDto>> Handle(ChatSessionGetAllQuery request, CancellationToken cancellationToken)
    {
        return _context
            .ChatSessions
            .AsNoTracking()
            .Where(x => x.AppUserId == _currentUserService.UserId)
            .OrderByDescending(cs => cs.CreatedOn)
            .Select(x => ChatSessionGetAllDto.MapFromChatSession(x))
            .ToListAsync(cancellationToken);
    }
}