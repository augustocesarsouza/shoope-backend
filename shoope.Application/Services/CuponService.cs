using AutoMapper;
using Shoope.Application.DTOs;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.Services.Interfaces;
using Shoope.Domain.Entities;
using Shoope.Domain.Repositories;
using System.Text.RegularExpressions;

namespace Shoope.Application.Services
{
    public class CuponService : ICuponService
    {
        private readonly ICuponRepository cuponRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICuponCreateDTOValidator _cuponCreateDTOValidator;

        public CuponService(ICuponRepository cuponRepository, IMapper mapper, IUnitOfWork unitOfWork,
            ICuponCreateDTOValidator cuponCreateDTOValidator)
        {
            this.cuponRepository = cuponRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cuponCreateDTOValidator = cuponCreateDTOValidator;
        }

        public async Task<ResultService<CuponDTO>> GetCuponById(Guid cuponId)
        {
            try
            {
                var cupon = await cuponRepository.GetCuponById(cuponId);

                return ResultService.Ok(_mapper.Map<CuponDTO>(cupon));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<CuponDTO>(ex.Message);
            }
        }

        public async Task<ResultService<CuponDTO>> CreateAsync(CuponDTO? cuponDTO)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                if (cuponDTO == null)
                    return ResultService.Fail<CuponDTO>("error dto is null");

                // Criar validação para criar "CuponDTO"
                var validateDTO = _cuponCreateDTOValidator.ValidateDTO(cuponDTO);

                if (!validateDTO.IsValid)
                    return ResultService.RequestError<CuponDTO>("validation error check the information", validateDTO);

                string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";

                if(cuponDTO.DateValidateCuponString == null)
                    return ResultService.Fail<CuponDTO>("DateValidateCuponString is null");

                if (!Regex.IsMatch(cuponDTO.DateValidateCuponString, pattern))
                    return ResultService.Fail<CuponDTO>("Error date informed is invalid DD/MM/YYYY");

                var cuponId = Guid.NewGuid();
                var stringCortada = cuponDTO.DateValidateCuponString.Split('/');

                var dia = stringCortada[0];
                var mes = stringCortada[1];
                var ano = stringCortada[2];

                var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                cuponDTO.SetCuponId(cuponId);
                cuponDTO.SetValueDateValidateCupon(birthDateUtc);

                var cupon = await cuponRepository.CreateAsync(_mapper.Map<Cupon>(cuponDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<CuponDTO>(cupon));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CuponDTO>(ex.Message);
            }
        }
    }
}
