using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;
public interface IMemberRepository
{
    Task<Member?> GetByEmailAsync(Email email,CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email,CancellationToken cancellationToken = default);

    void Add(Member member);
}