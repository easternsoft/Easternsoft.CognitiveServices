using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public class FaceEncodingManager
	{
		public static FaceEncodingManager Current = new FaceEncodingManager();

		private Dictionary<string, IEnumerable<FaceEncoding>> _cache;

		public FaceEncodingManager()
		{
			_cache = new Dictionary<string, IEnumerable<FaceEncoding>>();
		}

		public IEnumerable<FaceEncoding> Load(string imagePath, FaceRecognition faceRecognition)
		{
			if (_cache.ContainsKey(imagePath))
				return _cache[imagePath];

			Image image = null;
			if (File.Exists(imagePath))
			{
				image = FaceRecognition.LoadImageFile(imagePath);
			}
			else
			{
				image = LoadImageUrl(imagePath);
			}

			var encodings = faceRecognition.FaceEncodings(image);
			_cache.Add(imagePath, encodings);
			return encodings;
		}

		public Image LoadImageUrl(string imageUrl)
		{
			WebClient client = new WebClient();
			Stream stream = client.OpenRead(imageUrl);
			
			return stream.ToFaceRecognitionImage();
		}
	}
}
