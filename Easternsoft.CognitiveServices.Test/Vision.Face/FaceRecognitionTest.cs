using Easternsoft.CognitiveServices.Vision.Face;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Test.Vision.Face
{
	public class FaceRecognitionTest
	{
		private string _knownPeopleFolder = @"Vision.Face\Data\KnownPeople";
		private string _unknownPeopleFolder = @"Vision.Face\Data\UnknownPeople";

		[Test]
		public void GetFace_ShouldRunCorrectly()
		{
			FaceRecognitionManager faceRecognition = new FaceRecognitionManager();
			var faces = faceRecognition.GetFaces(_knownPeopleFolder, _unknownPeopleFolder);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual("biden", faces[0].Name);
			Assert.AreEqual("obama", faces[1].Name);
		}
	}
}
