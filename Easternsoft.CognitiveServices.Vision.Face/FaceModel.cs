using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Vision.Face
{
	public class FaceModel
	{
		public string ImagePath { get; set; }
		public string Name { get; set; }

		public static FaceModel ReadLine(string line)
		{
			var values = line.Split(',');
			return new FaceModel() {
				ImagePath = values[0],
				Name = values[1]
			};
		}
	}
}
