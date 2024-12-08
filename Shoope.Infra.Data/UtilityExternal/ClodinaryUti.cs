using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;
//using static System.Net.Mime.MediaTypeNames;

namespace Shoope.Infra.Data.UtilityExternal
{
    public class ClodinaryUti : ICloudinaryUti
    {
        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret);

        public async Task<CloudinaryCreate> CreateMedia(string url, string folder, int width, int height)
        {
            var cloudinary = new Cloudinary(_account);

            // Verifica se é uma imagem ou vídeo
            bool isImage = url.StartsWith("data:image");
            bool isVideo = url.StartsWith("data:video");

            if (isImage)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(url),
                    Transformation = new Transformation().Width(width).Height(height).Crop("fill").Quality(100),
                    Folder = folder
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return new CloudinaryCreate
                {
                    PublicId = uploadResult.PublicId,
                    ImgUrl = cloudinary.Api.UrlImgUp.BuildUrl(uploadResult.PublicId),
                };
            }
            else if (isVideo)
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(url),
                    Transformation = new Transformation().Width(width).Height(height).Crop("fill").Quality(100),
                    Folder = folder
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return new CloudinaryCreate
                {
                    PublicId = uploadResult.PublicId,
                    ImgUrl = cloudinary.Api.UrlVideoUp.BuildUrl(uploadResult.PublicId),
                };
            }
            else
            {
                throw new ArgumentException("Invalid media type. Only images and videos are supported.");
            }
        }

        public CloudinaryResult DeleteMediaCloudinary(string url, ResourceType resourceType, Cloudinary cloudinary)
        {
            try
            {
                var destroyParams = new DeletionParams(url) { ResourceType = resourceType };
                cloudinary.Destroy(destroyParams);

                if(destroyParams == null)
                    return new CloudinaryResult(false, false, "destroyParamsIsNull");

                return new CloudinaryResult(true, false, "delete successfully");
            }
            catch (Exception ex)
            {
                return new CloudinaryResult(false, true, ex.Message);
            }
        }
    }
}
