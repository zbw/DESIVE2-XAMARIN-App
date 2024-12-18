using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Desive2.Models
{
    public class ByteImage : Image
    {
        // Function to retrieve the image data as a byte array.
        public Func<byte[]> GetBytes { get; set; }
    }
}
