using Shoope.Infra.Data.CloudinaryConfigClass;

namespace Shoope.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateImg(string url, string folder, int width, int height);
    }
}
