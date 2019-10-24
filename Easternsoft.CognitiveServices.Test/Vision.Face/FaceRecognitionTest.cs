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
        private FaceRecognitionManager _faceRecognitionManager;

        public FaceRecognitionTest()
        {
            _faceRecognitionManager = FaceRecognitionManager.Current;
        }

        [Test]
		public void GetFaces_ShouldRunCorrectly()
		{
			var faces = _faceRecognitionManager.GetFaces(_knownPeopleFolder, _unknownPeopleFolder);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual("biden", faces[0].Name);
			Assert.AreEqual("obama", faces[1].Name);
		}

		[Test]
		public void GetFacesFromUrl_ShouldRunCorrectly()
		{
			var faces = _faceRecognitionManager.GetFaces(_obamaImage, _twoPeopleImage);

			Assert.AreEqual(2, faces.Count);
			Assert.AreEqual(true, faces[0].IsRecognized); // obama is recognized
			Assert.AreEqual(false, faces[1].IsRecognized); // biden is not recognized
		}

		[Test]
		public void GetFacesFromUrl_ShouldCahedCorrectly()
		{
			var faces = _faceRecognitionManager.GetFaces(_obamaImage, _twoPeopleImage);

            var before = DateTime.Now;
            var faces2 = _faceRecognitionManager.GetFaces(_obamaImage, _twoPeopleImage);
            var duration = DateTime.Now - before;
            Assert.Greater(4, duration.TotalSeconds);
        }
	}
}
