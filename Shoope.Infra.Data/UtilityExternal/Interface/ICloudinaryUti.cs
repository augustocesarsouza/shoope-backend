using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Shoope.Infra.Data.CloudinaryConfigClass;

namespace Shoope.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateMedia(string url, string folder, int width, int height);
        public CloudinaryResult DeleteMediaCloudinary(string url, ResourceType resourceType, Cloudinary cloudinary);
    }
}
