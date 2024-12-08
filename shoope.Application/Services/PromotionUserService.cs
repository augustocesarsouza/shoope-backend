using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class PromotionUserService : IPromotionUserService
    {
        private readonly IPromotionUserRepository _promotionUserRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromotionUserCreateDTOValidator _promotionUserCreateDTOValidator;

        public PromotionUserService(IPromotionUserRepository promotionUserRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IPromotionUserCreateDTOValidator promotionUserCreateDTOValidator)
        {
            _promotionUserRepository = promotionUserRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _promotionUserCreateDTOValidator = promotionUserCreateDTOValidator;
        }

        //public async Task<ResultService<List<PromotionUserDTO>>> GetById(Guid guidId)
        //{
        //    try
        //    {
        //        var getByIdAll = await _promotionUserRepository.GetById(guidId);

        //        return ResultService.Ok(_mapper.Map<List<PromotionUserDTO>>(getByIdAll));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResultService.Fail<List<PromotionUserDTO>>(ex.Message);
        //    }
        //}

        public async Task<ResultService<List<PromotionUserDTO>>> GetByUserIdAll(Guid guidId)
        {
            try
            {
                var getByIdAll = await _promotionUserRepository.GetByUserIdAll(guidId);

                return ResultService.Ok(_mapper.Map<List<PromotionUserDTO>>(getByIdAll));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<PromotionUserDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<PromotionUserDTO>> Create(PromotionUserDTO? promotionUserDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if(promotionUserDTO == null)
                    return ResultService.Fail<PromotionUserDTO>("DTO Is Null");

                var resultValidate = _promotionUserCreateDTOValidator.ValidateDTO(promotionUserDTO);

                if (!resultValidate.IsValid)
                    return ResultService.RequestError<PromotionUserDTO>("validation error check the information", resultValidate);

                var PromotionUserId = Guid.NewGuid();
                var PromotionUserCreate = new PromotionUser(PromotionUserId, promotionUserDTO.PromotionId, null, promotionUserDTO.UserId, null);

                var createPromotion = await _promotionUserRepository.CreateAsync(PromotionUserCreate);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<PromotionUserDTO>(createPromotion));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PromotionUserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<string>> Delete(Guid promotionId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var promotionUserRegister = await _promotionUserRepository.GetPromotionUserByPromotionId(promotionId);

                foreach (var itens in promotionUserRegister)
                {
                    var deleteSala = await _promotionUserRepository.DeleteAsync(itens);
                }

                await _unitOfWork.Commit();

                return ResultService.Ok<string>("delete Successfully");
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<string>(ex.Message);
            }
        }
    }
}
