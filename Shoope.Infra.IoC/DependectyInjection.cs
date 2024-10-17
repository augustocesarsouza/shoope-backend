using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shoope.Domain.Repositories;
using Shoope.Infra.Data.Context;
using Shoope.Infra.Data.Repositories;
using Shoope.Application.Mappings;
using Shoope.Application.Services.Interfaces;
using Shoope.Application.Services;
using Shoope.Application.DTOs.Validations.Interfaces;
using Shoope.Application.DTOs.Validations.UserValidator;
using Shoope.Infra.Data.SendEmailUser.Interface;
using Shoope.Infra.Data.SendEmailUser;
using Shoope.Infra.Data.UtilityExternal.Interface;
using Shoope.Infra.Data.UtilityExternal;
using Ingresso.Infra.Data.UtilityExternal;
using sib_api_v3_sdk.Api;
using Shoope.Application.DTOs.Validations.AddressValidator;
using Shoope.Domain.Authentication;
using Shoope.Infra.Data.Authentication;
using Shoope.Application.DTOs.Validations.Promotion;
using Shoope.Application.DTOs.Validations.CuponValidator;

namespace Shoope.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(
                  options => options.UseNpgsql(connectionString)); // Server=ms-sql-server; quando depender dele no Docker-Compose

            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionUserRepository, PromotionUserRepository>();

            services.AddScoped<ICuponRepository, CuponRepository>();
            services.AddScoped<IUserCuponRepository, UserCuponRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IPromotionUserService, PromotionUserService>();

            services.AddScoped<ICuponService, CuponService>();
            services.AddScoped<IUserCuponService, UserCuponService>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();
            services.AddScoped<IUserCreateAccountFunction, UserCreateAccountFunction>();
            services.AddScoped<ISendEmailUser, SendEmailUser>();
            services.AddScoped<ISendEmailBrevo, SendEmailBrevo>();
            services.AddScoped<ICacheRedisUti, CacheRedisUti>();
            services.AddScoped<ITransactionalEmailApiUti, TransactionalEmailApiUti>();
            services.AddScoped<ITokenGeneratorUser, TokenGeneratorUser>();
            services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();
            services.AddScoped<IUserSendCodeEmailDTOValidator, UserSendCodeEmailDTOValidator>();
            services.AddScoped<IPromotionUserCreateDTOValidator, PromotionUserCreateDTOValidator>();
            services.AddScoped<IAddressUpdateDTOValidator, AddressUpdateDTOValidator>();
            services.AddScoped<IAddressUpdateOnlyDefaultDTOValidator, AddressUpdateOnlyDefaultDTOValidator>();
            services.AddScoped<IAddressCreateDTOValidator, AddressCreateDTOValidator>();
            services.AddScoped<IPromotionCreateDTOValidator, PromotionCreateDTOValidator>();
            services.AddScoped<ICuponCreateDTOValidator, CuponCreateDTOValidator>();
            services.AddScoped<IPromotionCreateDTOIfPromotionNumber2Validator, PromotionCreateDTOIfPromotionNumber2Validator>();
            services.AddScoped<ICloudinaryUti, ClodinaryUti>();
            
            return services; 
        }
    }
}
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
//});