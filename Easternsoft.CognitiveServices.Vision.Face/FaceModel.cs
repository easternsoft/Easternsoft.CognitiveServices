using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Image = FaceRecognitionDotNet.Image;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public partial class FaceModel
	{
		public string ImagePath { get; set; }
		public string Name { get; set; }
		public bool IsRecognized { get; set; }
		public FaceEncoding Encoding { get; set; }

	}

	public partial class FaceModel
	{
		public static FaceModel Create(string imagePath, FaceEncoding encoding)
		{
			var model = new FaceModel();
			model.ImagePath = imagePath;
			model.Name = Path.GetFileNameWithoutExtension(imagePath);
			model.Encoding = encoding;

			return model;
		}
		public static List<FaceModel> Create(string imagePath, IEnumerable<FaceEncoding> encodings)
		{
			var lstModels = new List<FaceModel>();
			foreach (FaceEncoding encoding in encodings)
			{
				lstModels.Add(Create(imagePath, encoding));
			}

			return lstModels;
		}
		public static List<FaceModel> Load(string imagePath, FaceRecognition faceRecognition)
		{
			if (Directory.Exists(imagePath))
			{
				List<FaceModel> faceModels = new List<FaceModel>();
				var files = Directory.GetFiles(imagePath);
				foreach (string file in files)
				{
					faceModels.AddRange(Create(file, FaceEncodingManager.Current.Load(file, faceRecognition)));
				}

				return faceModels;
			}

			return Create(imagePath, FaceEncodingManager.Current.Load(imagePath, faceRecognition));
		}
	}
}
