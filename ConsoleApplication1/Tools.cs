﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesTextPlacer
{
	class Tools
	{
		public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
		{
			List<String> filesFound = new List<String>();
			var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			foreach (var filter in filters)
			{
				filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
			}
			filesFound.RemoveAll(_ => _.Contains("marked"));
			return filesFound.ToArray();
		}
	}
}
