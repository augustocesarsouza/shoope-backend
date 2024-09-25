using Shoope.Infra.Data.CloudinaryConfigClass;

namespace Shoope.Infra.Data.UtilityExternal.Interface
{
    public interface ICloudinaryUti
    {
        public Task<CloudinaryCreate> CreateImg(string url, int width, int height);
    }
}
