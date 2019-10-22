using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Test
{
	public class BaseTest
	{
		public string GetTestsPath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", string.Empty);
		}
	}
}
