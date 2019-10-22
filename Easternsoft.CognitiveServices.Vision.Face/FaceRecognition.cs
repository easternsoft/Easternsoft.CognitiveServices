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
	public class FaceRecognition
	{
		public List<FaceModel> GetFaces(string knownFaceFolder, string unknownFaceFolder)
		{
			var output = GetStdOutResult(knownFaceFolder, unknownFaceFolder);

			var lstFaces = new List<FaceModel>();
			var lines = output.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string line in lines)
			{
				lstFaces.Add(FaceModel.ReadLine(line));
			}

			return lstFaces;
		}

		public string GetStdOutResult(string knownFaceFolder, string unknownFaceFolder)
		{
			// Start the child process.
			Process p = new Process();
			// Redirect the output stream of the child process.
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;

			var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", string.Empty);
			knownFaceFolder = $"{dir}\\{knownFaceFolder}";
			unknownFaceFolder = $"{dir}\\{unknownFaceFolder}";

			p.StartInfo.FileName = dir + "\\CLITools\\face_recognition.exe";
			p.StartInfo.Arguments = $"{knownFaceFolder} {unknownFaceFolder}";
			p.Start();
			// Do not wait for the child process to exit before
			// reading to the end of its redirected stream.
			// p.WaitForExit();
			// Read the output stream first and then wait.
			string output = p.StandardOutput.ReadToEnd();
			p.WaitForExit();

			return output;
		}
	}
}
