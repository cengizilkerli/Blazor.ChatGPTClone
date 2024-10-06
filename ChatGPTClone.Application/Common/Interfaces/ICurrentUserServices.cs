namespace ChatGPTClone.Application.Common.Interfaces;

public interface ICurrentUserServices
{
    Guid UserId { get; }
    string IpAddress { get; }
}