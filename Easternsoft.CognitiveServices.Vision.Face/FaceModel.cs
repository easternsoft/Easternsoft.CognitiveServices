using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public class FaceModel
	{
		public string ImagePath { get; set; }
		public string Name { get; set; }
		public FaceEncoding Encoding { get; set; }
		public FaceModel()
		{

		}

		public static FaceModel Create(string imagePath, FaceEncoding encoding)
		{
			var model = new FaceModel();
			model.ImagePath = imagePath;
			model.Name = Path.GetFileNameWithoutExtension(imagePath);
			model.Encoding = encoding;

			return model;
		}
		public static  List<FaceModel> Create(string imagePath, IEnumerable<FaceEncoding> encodings)
		{ 
			var lstModels = new List<FaceModel>();
			foreach (FaceEncoding encoding in encodings)
			{
				lstModels.Add(Create(imagePath, encoding));
			}

			return lstModels;
		}

		public static List<FaceModel> LoadFromFile(string imagePath, FaceRecognition faceRecognition)
		{
			using (Image image = FaceRecognition.LoadImageFile(imagePath))
			{
				var encodingFaces = faceRecognition.FaceEncodings(image);
				return Create(imagePath, encodingFaces);
			}
		}
		public static List<FaceModel> LoadFromFolder(string imageFolder, FaceRecognition faceRecognition)
		{
			var lstModels = new List<FaceModel>();

			var files = Directory.GetFiles(imageFolder);
			foreach (string file in files)
			{
				lstModels.AddRange(LoadFromFile(file, faceRecognition));
			}

			return lstModels;
		}
	}
}
