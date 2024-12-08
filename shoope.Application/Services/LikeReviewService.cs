using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;

namespace Shoope.Application.Services
{
    public class LikeReviewService : ILikeReviewService
    {
        private readonly ILikeReviewRepository _likeReviewRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LikeReviewService(ILikeReviewRepository likeReviewRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _likeReviewRepository = likeReviewRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<List<LikeReviewDTO>>> GetByProductFlashSaleReviewsId(Guid productFlashSaleReviewsId)
        {
            try
            {
                var likeReviews = await _likeReviewRepository.GetByProductFlashSaleReviewsId(productFlashSaleReviewsId);

                return ResultService.Ok(_mapper.Map<List<LikeReviewDTO>>(likeReviews));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<List<LikeReviewDTO>>(ex.Message);
            }
        }

        public async Task<ResultService<LikeReviewDTO>> CreateAsync(LikeReviewDTO? likeReviewDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (likeReviewDTO == null)
                    return ResultService.Fail<LikeReviewDTO>("error dto null");

                var likeAlreadyExist = await _likeReviewRepository.AlreadyExistLike(likeReviewDTO.UserId, likeReviewDTO.ProductFlashSaleReviewsId);

                if (likeAlreadyExist != null)
                    return ResultService.Fail(new LikeReviewDTO(null, null, null, true));

                //ValidationResult validationAddress = _addressCreateDTOValidator.ValidateDTO(addressDTO);

                //if (!validationAddress.IsValid)
                //    return ResultService.RequestError<AddressDTO>("validation error check the information", validationAddress);

                var likeReviewCreate = await _likeReviewRepository.CreateAsync(_mapper.Map<LikeReview>(likeReviewDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<LikeReviewDTO>(likeReviewCreate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<LikeReviewDTO>(ex.Message);
            }
        }

        public async Task<ResultService<LikeReviewDTO>> DeleteAsync(LikeReviewDTO? likeReviewDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if(likeReviewDTO == null)
                    return ResultService.Fail<LikeReviewDTO>("error dto null");

                var likeAlreadyExist = await _likeReviewRepository.AlreadyExistLike(likeReviewDTO.UserId, likeReviewDTO.ProductFlashSaleReviewsId);

                if (likeAlreadyExist == null)
                    return ResultService.Fail(new LikeReviewDTO(null, null, null, true));

                var likeReview = await _likeReviewRepository.GetByUserId(likeReviewDTO.UserId);

                var likeReviewDelete = await _likeReviewRepository.DeleteAsync(_mapper.Map<LikeReview>(likeReview));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<LikeReviewDTO>(likeReviewDelete));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<LikeReviewDTO>(ex.Message);
            }
        }
    }
}
