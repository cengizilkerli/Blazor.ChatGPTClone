using ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll;
using ChatGPTClone.Application.Features.ChatSessions.Queries.GetById;

public interface IChatSessionCacheService
{
    Task<List<ChatSessionGetAllDto>> GetAllAsync(CancellationToken cancellationToken );
    Task<ChatSessionGetByIdDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    void Remove(Guid id);
}
