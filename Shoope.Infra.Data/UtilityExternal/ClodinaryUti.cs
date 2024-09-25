using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Shoope.Infra.Data.CloudinaryConfigClass;
using Shoope.Infra.Data.UtilityExternal.Interface;

namespace Shoope.Infra.Data.UtilityExternal
{
    public class ClodinaryUti : ICloudinaryUti
    {
        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret);

        public async Task<CloudinaryCreate> CreateImg(string url, int width, int height)
        {
            var cloudinary = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(url),
                Transformation = new Transformation().Width(width).Height(height).Crop("fill").Quality(100)
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            string publicId = uploadResult.PublicId;
            var imageUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

            var cloudinaryCreateImg = new CloudinaryCreate
            {
                PublicId = publicId,
                ImgUrl = imageUrl,
            };

            return cloudinaryCreateImg;
        }
    }
}
