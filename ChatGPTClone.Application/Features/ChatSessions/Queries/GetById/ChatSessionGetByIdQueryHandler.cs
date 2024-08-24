﻿using ChatGPTClone.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public class ChatSessionGetByIdQueryHandler : IRequestHandler<ChatSessionGetByIdQuery, ChatSessionGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public ChatSessionGetByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChatSessionGetByIdDto> Handle(ChatSessionGetByIdQuery request, CancellationToken cancellationToken)
    {
        var chatSession = await _context.ChatSessions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return ChatSessionGetByIdDto.MapFromChatSession(chatSession);
    }
}
