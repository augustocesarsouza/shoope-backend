using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class UserCuponService : IUserCuponService
    {
        private readonly IUserCuponRepository _userCuponRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserCuponService(IUserCuponRepository userCuponRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userCuponRepository = userCuponRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<List<UserCuponDTO>>> GetAllCuponByUserId(Guid userId)
        {
            try
            {
                var userCuponDTOAll = await _userCuponRepository.GetAllCuponByUserId(userId);

                return ResultService.Ok(_mapper.Map<List<UserCuponDTO>>(userCuponDTOAll));

            }catch (Exception ex)
            {
                return ResultService.Fail<List<UserCuponDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<UserCuponDTO>> Create(UserCuponDTO? userCuponDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if(userCuponDTO == null)
                    return ResultService.Fail<UserCuponDTO>("DTO Is Null");

                var createUserCupon = await _userCuponRepository.CreateAsync(_mapper.Map<UserCupon>(userCuponDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<UserCuponDTO>(createUserCupon));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserCuponDTO>(ex.Message);
            }
        }
    }
}
