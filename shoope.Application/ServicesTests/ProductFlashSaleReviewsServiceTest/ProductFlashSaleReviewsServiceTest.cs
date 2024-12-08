using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation.Results;
using Moq;
using Shoope.Application.DTOs;
using Shoope.Application.Services;
using Shoope.Domain.Entities;
using Shoope.Infra.Data;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Xunit;

namespace Shoope.Application.ServicesTests.ProductFlashSaleReviewsServiceTest
{
    public class ProductFlashSaleReviewsServiceTest
    {
        private readonly ProductFlashSaleReviewsServiceConfiguration _productFlashSaleReviewsServiceConfiguration;
        private readonly ProductFlashSaleReviewsService _productFlashSaleReviewsService1;

        public ProductFlashSaleReviewsServiceTest()
        {
            _productFlashSaleReviewsServiceConfiguration = new();
            var productFlashSaleReviewsService = new ProductFlashSaleReviewsService(
                _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock.Object,
                _productFlashSaleReviewsServiceConfiguration.MapperMock.Object,
                _productFlashSaleReviewsServiceConfiguration.UnitOfWorkMock.Object,
                _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsCreateDTOValidatorMock.Object,
                _productFlashSaleReviewsServiceConfiguration.CloudinaryUtiMock.Object
                );

            _productFlashSaleReviewsService1 = productFlashSaleReviewsService;
        }

        [Fact]
        public async Task Should_GetAllProductFlashSaleReviewsByProductFlashSaleId_Success()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(rep => rep.GetAllProductFlashSaleReviewsByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<ProductFlashSaleReviews>());

            var result = await _productFlashSaleReviewsService1.GetAllProductFlashSaleReviewsByProductFlashSaleId(productFlashSaleId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetAllProductFlashSaleReviewsByProductFlashSaleId()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(rep => rep.GetAllProductFlashSaleReviewsByProductFlashSaleId(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("error get all product flash sale by id"));

            var result = await _productFlashSaleReviewsService1.GetAllProductFlashSaleReviewsByProductFlashSaleId(productFlashSaleId);

            Assert.False(result.IsSucess);
            Assert.Equal("error get all product flash sale by id", result.Message);
        }

        [Fact]
        public async Task Should_CreateAsync_With_Img_Base64_Success()
        {
            var productFlashSaleReviewsDTO = new ProductFlashSaleReviewsDTO();
            var strings = new List<string>();
            strings.Add("data:image/webp;base64,UklGRrpEAABXRUJQVlA4WAoAAAAIAAAAXQIANwQAVlA4INpDAABQHgKdASpeAjgEPm00l0ikIqeoI9BpoQANiWl" +
                "u3Mtk/2++ctAPttVDDFxWclb6V4xKoil650NPnUaPyXaX0Xp5PZcEC5X43jw+i35l4PNYD9z+h35ZrBlfLhi7X/dL9BtRpr7i8vxdgr/ueWv6n0x/Gggw6cutkw3sWfdAy" +
                "Zt2z6rKTsna5uhGsVEJeH9ekz3J2Nb64ZjKGukKt7KG1UqAfjFDuWSZouAqlxZlw1TXpanNKHIJwr006Rr/KxF2KxGWKpyA4kI++uyyolopljmeGvk4n6maxnWeEfsCrj5RJ" +
                "5rq6XgsC1WC2d456BwSBNPvpqqR5kUfwUgcqwY5SxZnvHc1KcoK5l2U6pyxtMYu2CSWre+jUC66K7rpt8fiYhSIautdBgjO56We+4yifNOgzZIgXWdxMZEFvbzqDbq1lXVCNI" +
                "C+dR7AWjak8+8n1PaZnz+bDtxrxqEXzGE1dicy0m8HxR/0pBp1vhfjgqMX/DxJlC5cSaJ7wbJv6/+5y1D5X2ZmPYirwm55W/3yB4Z2Dm4dinNveNEcfp9vjNq5aww/5ajObzv/" +
                "IB0dulaEm2u6yVRfgq+yxlnpUTf4in+4qBuMV+GT/MEvWxeUvQhBPYpRqHPDlWZnsb+cq70BhqFUzfrZVGQ3vooIiAJIGBCozaOURlwMoxa9a8oH1f8Vd/IqTYMLZCZw30tYb/6sk" +
                "jQRQP4h3SzsVgUfZNm9q5O303ebfDdQJ3kCnVOfgBqE6SanGX6aauX9IL/d8tplwZAnIdL6WiO/pEfE/5XrlQiqDNeYDwAdfqOEEk4mBfGW5rLxRZ5xmE9FZ+Z8+A1nv6vh69UBuy" +
                "g4+vz9pOpODiOrJsh/FDvJaectKvKSNbL/zMErzEgG/thKDo1RxEwZzfH+5hL9ubDDh64QqKbAMDBjUIiJFbGZPXtKqAmUTuH5zG8YiLYskE4sLY9DjSnX99aev57X7xXJ1bQOIdTvu9G" +
                "WBTEA727+fEHqCj+3MocMhwqmEQH9JM5idcLbbwbZkaHne1gVXHvLzunOqOKZBu743fafH/6T+tnAlf9SaHXh1vIDEl2opvDlEKvIKo+zzbverSH6QsC7zXhq0qRzLRgyV5gWePenR+AIR" +
                "i3AkZQQvDRAj29WxTAVIjCNwU989ZsPHUdtim0uR2EZH9DV9GN42IhhwkOiSuimT4832RAucDnb1CUdDTAIaTL+M3tN6adIJZ02SDjZj74czn+vPPdhErro6KD88iACbK95Y0MxHEUAKx0qi58Wa" +
                "CJcpUeh8RtWyNmokrVoECVJ4S5YhvSgznRYDWqmUCOxJn7SruILpy6sXe2YfrgxCmusKDzG8QnKPixhy1vbmnI3BewrH8LeL3+uogdBMrWwaucUjnsVLJR8x0KIqeBmKNCyzuGzyd3mj9z09sEh9qA" +
                "T3hmOP7JZLmhELdViS8wXhkkKU5JFyTANfzHEAQOx5OvzxbbBZdwLEYRo5EBk85jF+PVW8RzVwmue/oU+NL4N9b1FA6y3OsBfg5KSxNYWCSYPpHQQO+F/u5r+70D7wpNWFVfggeBkZYeEvxx52q+QKef" +
                "bNk5naooqykht1HyR3+mNlvixNYZmSDseiYbeZGEo/XYaEvQTVbV+E1hqYUJFwpRnwFV1FiCrrtdF+YxqsH0aYKxo2iHtcI3/je+jmXscVjJiU+azczwylYAouFrNpTFd2FsPZc6JWKSegFE7GZFlAL39" +
                "1Ci9PLCpZ+W6vA0NX4iAS2pHnHihycVYCY6v9W4bnlHGzMoIO++ROaHwpSMa5ZEICiXdMX/lPuBD28EwOVsK6/gukgpZbfg4UC8SXkZif93NaC459yTBNvuhOe5ABNLIKbNqkV7r1AJfzZk6EMpAim61+SvP" +
                "ShEWaQfJ9+LMDUlNfDao93SngnuMRptudc/yADokWbm1Jy8if7dendCQbA1iOEkGXFam2BFBEW0XPeZfS9dzT2JrDLM3T/HAW3Rq7xTvkkPAJ+FUN6pqWSAYp+VQDSFzgg2HhTtkOaa8NwLhhWY3AQO1WmS" +
                "aaiedT5n4XdpzVpX89PhZlKwqabSIAJxcIF/ptEQ9G4H7xyCr7YeY9XRmM3Jj1Fe+sfqy6faeStkS/bjBt/mAtwKLX/xbj3vAxPMxv/68ZdX3eCyo5KOQtaHDJ700jISkUvCOTHV0V/oMAhJ28KuAMxaU/Bd" +
                "zA8nkoUsNDG0JSrCFQZxO2jQ8L+lA8SiLJHWSJRRQgJtmf4JFcvalaKfV9UGWl0VERPX/wSpzw+htMdv24ukwnu+nC2CGmCKHxvbnaMUZmE3Uwp2e34CViUgvXEiKc0Q52vQfQcoa1IPHVojrQXW1N3/O" +
                "Ukvcr8o9AyGkNxMbnCJdnziOWKZxrGarSSBRLk7PguyfD60OWuhGGW4mlaNL6ixLCqu89FC/OJ1RgpyeELwyYBTDs/f9B6JkByHZRkKz43IyB1YvQeqRFSPWfGebw8/UHLlfQVYhMMr8QWY3i+D1UqH" +
                "3Y10ITZJsFQFBddyq+zDxfRtdvZ6cqyEzdODVVaeOJcKlh1imsBXLqpAczFC9DcdGSA89I7ALwiLEgazahq9HjxGpmxZrNx3Ojkz1qjmHyHjcQlvNskJd3R6Xwj1U9Ig7BJaxG+nCDzK8AOwFyD8jY" +
                "p3Z8F2fdn67HOUlidPfL2RTEOc8SJVIJuDw8ASnokCoxCKK2i4HuBPMFYncwTCIW50ju8GequYCJHrbU45lfUYrVMmLF5yql4g9yBrp4dqCrY10dreY8fa3e+A8wz68gPd5uFDKG40MmnbTLZdXA" +
                "6A8S8RoMQI7JrrOXntqts1n1MtUtCSnC0FLK9J0XjcRWssYS5+KyyNugcDOqFzOgVPOGpiXmce59ngLNEfIltVw8WVHOBCBg6VKlTXj6txC8mFCATSaVRwBNvsakp5LAIQ9TyWFeaPVW0fM/LdYv" +
                "ywmnjWGe4pKPYJ8M1hqXbhr0yGdQ08jYDdq3L4ihTmNRdpXJwEgmyFWy58KM5nnzvaa3FV6T9CXmYKw5q98Yx9IwfSfa802Gtcu/EbMFIgmSGfDh7TdmoMpdV5Ntc1fMjhFJFQbtOs+c4lbFbMVv" +
                "Kpd81irxl0kkVkQdZJsJ/jcNDy3U1TqSFROyql4H3b9rvuFiBRNFojgc4E3j/zbXD+mfvzv3njibkvFMpJXMMExV2Im3XLKq0LvrA+nvAfiMIED1tw/WGPFyNc/rZF+KIWlWXL6IWJp2CRYVko5w" +
                "063ZLZQweKLDylEuea/PVkdV8PJABYvMMzlwkVHuyAK/7uPNz/0BeIOIQgnySumODy/8QTHsrUJCpE8t0GvNCzDqu8lpKtX4olGTOaFCGswLYisTuv85eyL5mwZsQx2jhODaIZNZ+60t40nzK2" +
                "LirvWNPG015dZqMuB7Cqp4xu5vBfpNi8wv/9128o3x5t/gD0rNYzjDjnbEVhdezMHzgqwPE79ldOSfKFrKGNIqyBETps9YJF263l7dLvBGRJ75myVXIhmI6Dw728AhEjs+mlSQF4L2TkVpGf/BEA" +
                "3tP+aCDi+Nd+G8Y/h93hI564uvv+lXL21eziP/q3EOPgPvfEzO0pCTqfBxHqbxkG8TxCJ+h/MLo+e8ZryfQ0yeQzgfWXYBYnDbbnin8N4BHTGks5/tVMyoEet2iTkCZHRa4fVAaaOKunSZQn7j+uOyn5G+AYG" +
                "Q/V2BRycbsvsh0KRCnEvnXWp5MbVFtvE+VRJ1KqBwDGOxyqdudWdut6+fHZBjcjIQQYGQgYxj7mdsu+p79oGyqW93jJbto6JX+S1vFZXAjtpQBQrVlsfhX0Pmyypvb/Cxtgkj5JE4mxnMl9KWQ" +
                "GmopWYOPR3GrizlXZiVUT7CsEulecMgy/GsNTLPDVUEIGDk5SiQdw6inkl+6bBADIMhBBgZAet+EJOqAYGQgXm38N0YIh2Gx+jMoamHZUhJ1PhCTqvStTDutIMRa5TDx3xZWT106Gph2VH" +
                "9jBJaADnAhAwclOe9YnkWi7cpc/sZYFLhp4/hNYamWeBWlYx9yphqYdmWBbLeMrQPYK8i1Lx/fhNYapsJrDbqxYpAvgyEDGFUuUcjE8KkKfVg5/AjrGYXMOyo5MDz0sWNsJigYOSksTdcvS06vxJv9ytIsqDzT1ja0a" +
                "qBg5KTb8HJTnvjGPqfByUlbMail6t2/QkcDp7uyjHgCQdryf+MXvg5KTbJKrVM/dypX1KOSkrZg+Qh59CUflcqySzyzsZUjIb4OSk2yI9eOvdgYGQgYOSkg6jLVW/S0WzDIh27qG03poTWGplnhkGX4" +
                "1uKvwmsNTDjPT0yoIDjwlIvqszX8HisARJYmtxV+ETDMzuh5iIlkbcYJ14AkBMoJIaOybKD86P0MoXiyx9BeaMkKloAOSkzz37wYwK/wpxU1K5V21loKFXzfNP7EtydDAUfi3P9qGKBe3GvrU" +
                "Z2nPjnKSOFdx15AYx6ZsEep/FzeaaQN5lyZu4My2B5IQIbgn2jwQweFIDYyZTdHZ9P5dEjcvNgkr89DYKt9cNcCLVTWGpgjNY9xZX4kDMhHuX3welnbfN9sh/wQ9eg5edsTVjK/hxMxaLDaoHzlJH" +
                "up75Jt64F8GQgZv0BitEB2tt8nrhw92Y1fTxxE7/52LzW9O3lSEGETagMCUqUlWcuY+fLKir8JrDVNhNbiq5KjpugXLNiSg+QzqQPR2tULIpvJ/Z1r8weQd3+rREMm4PdQx/2kRTvD+qtKPYOtemHad+nm" +
                "qASfGxuIJjtwPpDi1BhxvhdZtgz1u3dpgy5+W3znGo3nQcOa5YgGCbAZNmD6SkH7btkXn8IFQvA7s1OdZlfEAdlNiEzC/wNj5xAshY6OFSzTxBOJqOQKOpYcigM3U5dRe7SMYx0Za6bohXD5Ahd" +
                "FCbyndLoTgXV+ruer+912ZCaVBHOmHZTklSMMDeBPuvK4Z8eqxbY9Rh3++rv4S5UAuy3IXqOxjNNpOfMoFQ2r1n30BvKvP6g2HUemyzSCToI9+Ni83CBFqMvMhAwclEJUmLCuw5FRtN9" +
                "NV/2oSfrlFzoLPaQB8HoYEEaQMVB0YZbNAnL6/Qb786yehwfghpA26IBtj6FRaCsnIJduF1gSUn3DAOynJMNRI34/1ZJ8krgbl7x8h6UWBrdBLvpsiaCXg0Uks4aivEaCjHMVujWDO0" +
                "svLboa8Gsfn96CXK3IYD5XodvWb5yteZIQA6AN4zGHH+RrvllsIIbr+z+bBnTdG5s/BwDS8ZHfuWNeVSahgIHRPCtFfc9pEG50WPEaHhSspkfqsc4bLVWpgwDjhnzsQujWrb5J8Km6I8ExcE" +
                "CLdYZ/GMUbWVVbVxdmjUJXUov21UyzR6nyNTucWH/FTe5xO+T3WZtogDsxiZ0+q+GTeRdKPpxhIlp0r0IL5vSQe2vAhRZSfjaDLiHvpAs4li84iYU3m5ejG13d9wDG8lwhJNN9kne/37WnHGWoS" +
                "pte6BN+VGhEVWbjK4kJtMYn4GfS5LNHJ+iMCBE+z/BHYwRXQXGQUcQgHGgZTQODvTRQmSmeDx5xi+UAGsuGMEUKAgV7vKGGBSwKs4XgZMYLT7yJ6nnvdXzHlabaNDoP7Cp1Kkx2mGLcYBasxwyJ7" +
                "N2ib0SOqHIn2zY7xPQf5G28LwVLK2PsWlQNiSO0uvgzjtWlQsUz/CEdaeb4ihPipv6n/A0yyzIS0nJday7nSsGbDuJnZq/AP+Ayk1A3Y/g9MIzH88xxym+eUAqSX/ajp3tiPeblD4ATnsfZUcCp2wzbM1W" +
                "UuV4MTvpVgiGTzipLocEnGGlshX2OyDwnGQxAAA/v2rXcDJorjnbeF0fE2uIqmCX/kNBYHgG1Xae6izZLGXR7u5zDj0O9aw/q2PGnBsXQ5lz+KRpZPdGTUj0AvwHiBv80vCP+bgzNaj3sooY9PmXu1eF4" +
                "ADwJ2NVqVsKVfqJi2w1aVcBvQh1jO0OjVxpE0VVsnRWnUy5GUqBzqWfyPO0F+hw8L6a//7BmbCqMQM54Xb+j7GaM4ylNtvs57ruHM/xZFXO5KGCBRgGMd9F4QLbAIMnSJQUZAmkxH3w2ilZV3JSZDp7Gg9" +
                "GhPwF9y5DbhxOxKhKiiphykbAZiWwVS8NkRnsisReocSvMv9ealZQlnR+gmbC6moeZVEqrIMt6IasRS8903D2B1UArjRNDQx3kykClVwd6B05s4NkYrxqmCflgg4E1kTI3jL48r3L0wFXSfrCwxxyyRWTq" +
                "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");

            productFlashSaleReviewsDTO.SetImgAndVideoReviewsProductElements(strings);

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductFlashSaleReviewsDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "ImgUrl1";
            cloudinaryCreate.PublicId = "PublicId1";

            _productFlashSaleReviewsServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productFlashSaleReviewsService1.CreateAsync(productFlashSaleReviewsDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_CreateAsync_With_Video_Base64_Success()
        {
            var productFlashSaleReviewsDTO = new ProductFlashSaleReviewsDTO();
            var strings = new List<string>();
            strings.Add("data:video/mp4;base64,AAAAIGZ0eXBpc29tAAACAGlzb21pc2=");

            productFlashSaleReviewsDTO.SetImgAndVideoReviewsProductElements(strings);

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductFlashSaleReviewsDTO>()))
                .Returns(new ValidationResult());

            var cloudinaryCreate = new CloudinaryCreate();
            cloudinaryCreate.ImgUrl = "VideoUrl1";
            cloudinaryCreate.PublicId = "VideoPublicId1";

            _productFlashSaleReviewsServiceConfiguration.CloudinaryUtiMock
                .Setup(rep => rep.CreateMedia(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(cloudinaryCreate);

            var result = await _productFlashSaleReviewsService1.CreateAsync(productFlashSaleReviewsDTO);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Throw_Exception_Error_When_Convert_FromBase64String_CreateAsync()
        {
            var productFlashSaleReviewsDTO = new ProductFlashSaleReviewsDTO();
            var strings = new List<string>();
            strings.Add("data:image/webp;base64,UklGRrpEAABXRUJQVlA4WAoAAAAIAA=");

            productFlashSaleReviewsDTO.SetImgAndVideoReviewsProductElements(strings);

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductFlashSaleReviewsDTO>()))
                .Returns(new ValidationResult());

            var result = await _productFlashSaleReviewsService1.CreateAsync(productFlashSaleReviewsDTO);
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_DTO_Is_Null_CreateAsync()
        {
            var result = await _productFlashSaleReviewsService1.CreateAsync(null);
            Assert.False(result.IsSucess);
            Assert.Equal("error DTO is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_When_ValidateDTO_CreateAsync()
        {
            var productFlashSaleReviewsDTO = new ProductFlashSaleReviewsDTO();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsCreateDTOValidatorMock
                .Setup(valid => valid.ValidateDTO(It.IsAny<ProductFlashSaleReviewsDTO>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                            {
                            new ValidationFailure("PropertyName", "Error message 1"),
                            }));

            var result = await _productFlashSaleReviewsService1.CreateAsync(productFlashSaleReviewsDTO);
            Assert.False(result.IsSucess);
            Assert.Equal("validation error check the information", result.Message);
        }

        [Fact]
        public async Task Should_Delete_Successfully()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.GetByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductFlashSaleReviews());

            var strings = new List<string>();
            strings.Add("http://res.cloudinary.com/dyqsqg7pk/video/upload/v1/reviews-product-flash-sale-img-and-video/qdo46hzuyilvatdz7vgw,http://res.cloudinary.com/dyqsqg7pk/image/upload/v1/reviews-product-flash-sale-img-and-video/vtqs4guzvcvuf9t4ovu7");

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.DeleteAsync(It.IsAny<ProductFlashSaleReviews>()))
                .ReturnsAsync(new ProductFlashSaleReviews(null, null, null, null, null, null,
                null, null, null, strings, null));

            _productFlashSaleReviewsServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.DeleteMediaCloudinary(It.IsAny<string>(), It.IsAny<ResourceType>(), It.IsAny<Cloudinary>()))
                .Returns(new CloudinaryResult(true, false, ""));

            var result = await _productFlashSaleReviewsService1.Delete(productFlashSaleId);
            Assert.True(result.IsSucess);
        }

        [Fact]
        public async Task Should_Return_Error_GetByProductFlashSaleId_Not_Found()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.GetByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync((ProductFlashSaleReviews?)null);

            var result = await _productFlashSaleReviewsService1.Delete(productFlashSaleId);

            Assert.False(result.IsSucess);
            Assert.Equal("error promotion not found", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_DeleteAsync_Obj_ImgAndVideoReviewsProduct_Is_Null()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.GetByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductFlashSaleReviews());

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.DeleteAsync(It.IsAny<ProductFlashSaleReviews>()))
                .ReturnsAsync(new ProductFlashSaleReviews(null, null, null, null, null, null,
                null, null, null, null, null));

            var result = await _productFlashSaleReviewsService1.Delete(productFlashSaleId);

            Assert.False(result.IsSucess);
            Assert.Equal("error ImgAndVideoReviewsProduct is null", result.Message);
        }

        [Fact]
        public async Task Should_Return_Error_DeleteMediaCloudinary_DeleteSuccessfully_false()
        {
            Guid productFlashSaleId = Guid.NewGuid();

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.GetByProductFlashSaleId(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductFlashSaleReviews());

            var strings = new List<string>();
            strings.Add("http://res.cloudinary.com/dyqsqg7pk/video/upload/v1/reviews-product-flash-sale-img-and-video/qdo46hzuyilvatdz7vgw,http://res.cloudinary.com/dyqsqg7pk/image/upload/v1/reviews-product-flash-sale-img-and-video/vtqs4guzvcvuf9t4ovu7");

            _productFlashSaleReviewsServiceConfiguration.ProductFlashSaleReviewsRepositoryMock
                .Setup(valid => valid.DeleteAsync(It.IsAny<ProductFlashSaleReviews>()))
                .ReturnsAsync(new ProductFlashSaleReviews(null, null, null, null, null, null,
                null, null, null, strings, null));

            _productFlashSaleReviewsServiceConfiguration.CloudinaryUtiMock
                .Setup(valid => valid.DeleteMediaCloudinary(It.IsAny<string>(), It.IsAny<ResourceType>(), It.IsAny<Cloudinary>()))
                .Returns(new CloudinaryResult(false, false, ""));

            var result = await _productFlashSaleReviewsService1.Delete(productFlashSaleId);

            Assert.False(result.IsSucess);
            Assert.Equal("error when delete image", result.Message);
        }
    }
}
