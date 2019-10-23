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
		public  FaceRecognition FaceRecognition
		{
			get 
			{
				if (_faceRecognition == null)
				{
					_faceRecognition = FaceRecognition.Create(FaceRecognitionModels.Path);
				}

				return _faceRecognition;
			}
		}

		public FaceRecognitionManager()
		{
			FaceRecognition.InternalEncoding = System.Text.Encoding.GetEncoding("utf-8");
		}

		public List<FaceModel> GetFaces(string knownFaceFolder, string unknownFaceFolder)
		{
			var knownFaceModels = FaceModel.LoadFromFolder(knownFaceFolder, FaceRecognition);
			var unknowFaceModels = FaceModel.LoadFromFolder(unknownFaceFolder, FaceRecognition);

			RecogniteUnknownFaceModels(knownFaceModels, unknowFaceModels);
			return unknowFaceModels;
		}

		public List<FaceModel> GetFacesFromUrl(string knownFaceUrl, string unknownFaceUrl)
		{
			var knownFaceModels = FaceModel.LoadFromUrl(knownFaceUrl, FaceRecognition);
			var unknowFaceModels = FaceModel.LoadFromUrl(unknownFaceUrl, FaceRecognition);

			RecogniteUnknownFaceModels(knownFaceModels, unknowFaceModels);
			return unknowFaceModels;
		}

		public void RecogniteUnknownFaceModels(List<FaceModel> knownFaceModels, List<FaceModel> unknowFaceModels)
		{
			foreach (FaceModel knownFaceModel in knownFaceModels)
			{
				foreach (FaceModel unknowFaceModel in unknowFaceModels)
				{
					RecogniteUnknownFaceModels(knownFaceModel, unknowFaceModel);
				}
			}
		}
		public void RecogniteUnknownFaceModels(FaceModel knownFaceModel, FaceModel unknowFaceModel)
		{
			if (unknowFaceModel.IsRecognized == false)
			{
				if (FaceRecognition.CompareFace(knownFaceModel.Encoding, unknowFaceModel.Encoding))
				{
					unknowFaceModel.Name = knownFaceModel.Name;
					unknowFaceModel.IsRecognized = true;
				}
				else
				{
					unknowFaceModel.Name = "unknown";
				}
			}
		}
	}
}
