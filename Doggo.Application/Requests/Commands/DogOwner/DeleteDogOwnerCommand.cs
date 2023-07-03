namespace Doggo.Application.Requests.Commands.DogOwner;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteDogOwnerCommand(Guid DogOwnerId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteDogOwnerCommand, CommonResult>
    {
        private readonly IDogOwnerRepository _dogOwnerRepository;


        public Handler(IDogOwnerRepository dogOwnerRepository)
        {
            _dogOwnerRepository = dogOwnerRepository;
        }

        public async Task<CommonResult> Handle(DeleteDogOwnerCommand request, CancellationToken cancellationToken)
        {
            var dogOwner = await _dogOwnerRepository.GetAsync(request.DogOwnerId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _dogOwnerRepository.Remove(dogOwner);

            return Success();
        }
    };
};