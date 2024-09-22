using ChatGPTClone.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public class ChatSessionGetByIdQueryValidator : AbstractValidator<ChatSessionGetByIdQuery>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserServices _currentUserService;

    public ChatSessionGetByIdQueryValidator(IApplicationDbContext context, ICurrentUserServices currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public ChatSessionGetByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(BeValidIdAsync)
            .WithMessage("The selected Chat was not found!");
    }

    private Task<bool> BeValidIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.ChatSessions.AnyAsync(x => x.Id == id && x.AppUserId == _currentUserService.UserId, cancellationToken);
    }
}
