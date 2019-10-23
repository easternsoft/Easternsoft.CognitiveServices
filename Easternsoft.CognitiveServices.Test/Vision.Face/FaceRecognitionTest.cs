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
		private string _obamaImage = "https://i.imgur.com/VLhPcj5.png";
		private string _twoPeopleImage = "https://i.imgur.com/sddIYpq.png";

		[Test]
		public void GetFaces_ShouldRunCorrectly()
		{
			FaceRecognitionManager faceRecognition = new FaceRecognitionManager();
			var faces = faceRecognition.GetFaces(_knownPeopleFolder, _unknownPeopleFolder);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual("biden", faces[0].Name);
			Assert.AreEqual("obama", faces[1].Name);
		}

		[Test]
		public void GetFacesFromUrl_ShouldRunCorrectly()
		{
			FaceRecognitionManager faceRecognition = new FaceRecognitionManager();
			var faces = faceRecognition.GetFacesFromUrl(_obamaImage, _twoPeopleImage);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual(true, faces[0].IsRecognized); // obama is recognized
			Assert.AreEqual(false, faces[1].IsRecognized); // biden is not recognized
		}
	}
}
