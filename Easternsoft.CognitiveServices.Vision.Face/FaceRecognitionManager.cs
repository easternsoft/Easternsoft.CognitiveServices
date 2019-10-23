using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public class FaceRecognitionManager
	{
		private FaceRecognition _faceRecognition;
		public FaceRecognitionManager()
		{
			FaceRecognition.InternalEncoding = System.Text.Encoding.GetEncoding("utf-8");
			_faceRecognition = FaceRecognition.Create(FaceRecognitionModels.Path);

		}

		public List<FaceModel> GetFaces(string knownFaceFolder, string unknownFaceFolder)
		{
			var knownFaceModels = FaceModel.LoadFromFolder(knownFaceFolder, _faceRecognition);
			var unknowFaceModels = FaceModel.LoadFromFolder(unknownFaceFolder, _faceRecognition);

			RecogniteUnknownFaceModels(knownFaceModels, unknowFaceModels);
			return unknowFaceModels;
		}

		public void RecogniteUnknownFaceModels(List<FaceModel> knownFaceModels, List<FaceModel> unknowFaceModels)
		{
			foreach (FaceModel knownFaceModel in knownFaceModels)
			{
				foreach (FaceModel unknowFaceModel in unknowFaceModels)
				{
					if (FaceRecognition.CompareFace(knownFaceModel.Encoding, unknowFaceModel.Encoding))
					{
						unknowFaceModel.Name = knownFaceModel.Name;
					}
				}
			}
		}
	}
}
