using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Features.Members.CreateMember;
public sealed record CreateMemberCommand(CreateMemberRequestDto Dto) : ICommand<Guid>;

public record CreateMemberRequestDto(string Email,string FirstName,string LastName);

internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand,Guid>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<Guid>> Handle(CreateMemberCommand request,CancellationToken cancellationToken)
    {
        var createEmail = Email.CreateEmail(request.Dto.Email);

        if(!await _memberRepository.IsEmailUniqueAsync(createEmail.Value,cancellationToken))
        {
            return Result.Failure<Guid>(DomainErrors.Member.EmailAlreadyInUse);
        }

        var member = Member.Create(Guid.NewGuid(),createEmail.Value,request.Dto.FirstName,request.Dto.LastName);

        _memberRepository.Add(member);

        return Result.Success(member.Id);
    }
}