﻿using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Service
{
    public sealed class EditServiceCommandHandler : ICommandHandler<EditServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditServiceCommand command)
        {
            var validationResult = await new EditServiceCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var Service = await _unitOfWork.ServicesRepository.GetAsync(command.Id);
            if (Service == null)
            {
                return Result.Fail("Service does not exist.");
            }

            _mapper.Map(command, Service);

            await _unitOfWork.ServicesRepository.UpdateAsync(Service);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
