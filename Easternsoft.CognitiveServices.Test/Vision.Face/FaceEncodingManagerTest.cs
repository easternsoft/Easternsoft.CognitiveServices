using Easternsoft.CognitiveServices.Vision.Face;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Test.Vision.Face
{
	public class FaceEncodingManagerTest
	{
		private FaceEncodingManager _faceEncodingManager;

		public FaceEncodingManagerTest()
		{
			_faceEncodingManager = FaceEncodingManager.Current;
		}


		[TestCase("https://i.imgur.com/VLhPcj5.png")]
		[TestCase("https://i.imgur.com/kVBXV1L.png")]
		[TestCase("https://scontent.xx.fbcdn.net/v/t1.0-9/156039_173862902961691_1001095764176344961_n.jpg?_nc_cat=105&_nc_oc=AQk5F6fJZdXG-SVr-gbFjzLGqG6hrhKjuqX727wGj1-J3rFrYU2mNRCArnNEk4QldsE&_nc_ad=z-m&_nc_cid=0&_nc_zor=9&_nc_ht=scontent.xx&oh=a4892d574e0b972c0606e0425543f6d9&oe=5E657A18")]
		public void LoadImageUrl_ShouldRunCorrectly(string imageUrl)
		{
			var image = _faceEncodingManager.LoadImageUrl(imageUrl);
		}

		[TestCase("https://i.imgur.com/VLhPcj5.png", 1)]
		[TestCase("https://www.evernote.com/shard/s545/sh/94cd3755-78dc-4c84-b812-d2c93688f79a/15e2fb243d56fa54/res/e93b50fc-70f1-4420-86c1-259c132b84fe/60711755_10214111802212384_1136949414586220544_n.jpg", 1)]
		[TestCase("https://scontent.xx.fbcdn.net/v/t1.0-9/156039_173862902961691_1001095764176344961_n.jpg?_nc_cat=105&_nc_oc=AQk5F6fJZdXG-SVr-gbFjzLGqG6hrhKjuqX727wGj1-J3rFrYU2mNRCArnNEk4QldsE&_nc_ad=z-m&_nc_cid=0&_nc_zor=9&_nc_ht=scontent.xx&oh=a4892d574e0b972c0606e0425543f6d9&oe=5E657A18", 3)]
		public void Load_ShouldRunCorrectly(string imageUrl, int expectedFaces)
		{
			var faceEncodings = _faceEncodingManager.Load(imageUrl, FaceRecognitionManager.Current.FaceRecognition);

			Assert.AreEqual(expectedFaces, faceEncodings.ToList().Count);
		}
	}
}
