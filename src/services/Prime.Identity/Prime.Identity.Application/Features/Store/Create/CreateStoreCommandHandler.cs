using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Application.Features.Store.Create;

internal sealed class CreateStoreCommandHandler(
    IRepository<Domain.Entities.Stores.Store> repository,
    ICurrentUserService currentUserService,
    IImageService imageService) : ICommandHandler<CreateStoreCommand,bool>
{
    private readonly IRepository<Domain.Entities.Stores.Store> _repository = repository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IImageService _imageService = imageService;

    public async Task<Result<bool>> Handle(CreateStoreCommand command,CancellationToken ct)
    {

        var userId = UserId.TryParse(_currentUserService.UserId,out var parsedUserId)
    ? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        string imagePath = string.Empty;

        if(command.Request.Image is not null)
            imagePath = await _imageService.SaveImageAsync(command.Request.Image);

        string imageCoverPath = string.Empty;

        if(command.Request.ImageCover is not null)
            imageCoverPath = await _imageService.SaveImageAsync(command.Request.ImageCover);

        var store = Domain.Entities.Stores.Store.Create(
            userId,
            command.Request.CategoryId,
            command.Request.Name,
            command.Request.Description,
            imageCoverPath,
            imagePath,
            command.Request.Address,
            command.Request.IsShippingAvailable,
            command.Request.City);

        await _repository.AddAsync(store,ct);

        return Result.Success(true);
    }

}