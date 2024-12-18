using System.IO;
using System.Threading.Tasks;

namespace Desive2.Services
{
    /// <summary>
    /// Defines a contract for a service that allows the user to pick a photo from their device.
    /// </summary>
    public interface IPhotoPickerService
    {
        /// <summary>
        /// Asynchronously retrieves the image stream of a photo selected by the user from their device.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The result is a <see cref="Stream"/> representing the image, or <c>null</c> if no image is selected.</returns>
        Task<Stream> GetImageStreamAsync();
    }

}
