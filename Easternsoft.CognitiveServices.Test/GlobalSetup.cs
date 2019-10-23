using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easternsoft.CognitiveServices.Test
{
	[SetUpFixture]
	public class GlobalSetup
	{
		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
			// or identically under the hoods
			Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
		}
	}
}
