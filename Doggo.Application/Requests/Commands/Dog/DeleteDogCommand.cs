namespace Doggo.Application.Requests.Commands.Dog;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteDogCommand(Guid DogId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteDogCommand, CommonResult>
    {
        private readonly IDogRepository _dogRepository;


        public Handler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<CommonResult> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogOwner = await _dogRepository.GetAsync(request.DogId, cancellationToken);

            if (dogOwner is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _dogRepository.Remove(dogOwner);

            return Success();
        }
    };
};