using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Image = FaceRecognitionDotNet.Image;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public static class Extension
	{
		public static Image ToFaceRecognitionImage(this Bitmap bitmap)
		{
			BitmapData bmpdata = null;

			try
			{
				bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
				int numbytes = bmpdata.Stride * bitmap.Height;
				byte[] bytedata = new byte[numbytes];
				IntPtr ptr = bmpdata.Scan0;

				Marshal.Copy(ptr, bytedata, 0, numbytes);

				return FaceRecognition.LoadImage(bytedata, bitmap.Height, bitmap.Width, 3);
			}
			finally
			{
				if (bmpdata != null)
					bitmap.UnlockBits(bmpdata);
			}
		}

		public static Image ToFaceRecognitionImage(this Stream stream)
		{
			Bitmap bitmap = new Bitmap(stream);
			return bitmap.ToFaceRecognitionImage();
		}
	}
}
