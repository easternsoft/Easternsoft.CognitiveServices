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
            Assert.Greater(12, duration.TotalSeconds);
        }

		[TestCase("https://www.evernote.com/shard/s545/sh/94cd3755-78dc-4c84-b812-d2c93688f79a/15e2fb243d56fa54/res/e93b50fc-70f1-4420-86c1-259c132b84fe/60711755_10214111802212384_1136949414586220544_n.jpg", "https://scontent.xx.fbcdn.net/v/t1.0-9/60954126_10214111802252385_6576470343295172608_o.jpg?_nc_cat=104&_nc_oc=AQkF2A0DohoEhaobai6GmGrkyt5gbbjSe6fYldagA5Mg9B2kwRy-PCeRd6B4b_o1-N8&_nc_ad=z-m&_nc_cid=0&_nc_zor=9&_nc_ht=scontent.xx&oh=8a1fb3768777f10e011f14937c67eb8d&oe=5E60C796", true)]
		public void GetFacesFromUrl_ShouldRunCorrectly(string knowFaceUrl, string unknownFaceUrl, bool expected)
		{
			var faces = _faceRecognitionManager.GetFaces(knowFaceUrl, unknownFaceUrl);

			Assert.AreEqual(expected, faces.Any(face => face.IsRecognized == true));
		}
	}
}
