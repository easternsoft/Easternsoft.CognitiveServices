using Easternsoft.CognitiveServices.Vision.Face;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
		public void GetStdOutResult_ShouldRunCorrectly()
		{
			FaceRecognition faceRecognition = new FaceRecognition();
			var output = faceRecognition.GetStdOutResult(_knownPeopleFolder, _unknownPeopleFolder);

			Assert.IsTrue(output.Contains("biden"));
			Assert.IsTrue(output.Contains("obama"));
		}

		[Test]
		public void GetFace_ShouldRunCorrectly()
		{
			FaceRecognition faceRecognition = new FaceRecognition();
			var faces = faceRecognition.GetFaces(_knownPeopleFolder, _unknownPeopleFolder);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual("biden", faces[0].Name);
			Assert.AreEqual("obama", faces[1].Name);
		}
	}
}
