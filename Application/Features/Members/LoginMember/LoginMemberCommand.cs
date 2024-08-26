using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using Domain.ValueObjects;

public record LoginMemberCommand(LoginMemberRequestDto Dto) : ICommand<string>;

public record LoginMemberRequestDto(string Email,string Password);

internal sealed class LoginMemberCommandHandler(IJwtProvider jwtProvider,IMemberRepository memberRepository) : ICommandHandler<LoginMemberCommand,string>
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<Result<string>> Handle(LoginMemberCommand request,CancellationToken cancellationToken)
    {
        var createEmail = Email.CreateEmail(request.Dto.Email);

        var member = await _memberRepository.GetByEmailAsync(createEmail.Value,cancellationToken);

        if(member is null)
        {
            return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
        }

        string token = _jwtProvider.Generate(member);

        return Result.Success(token);
    }
}