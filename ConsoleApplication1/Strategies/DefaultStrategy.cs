using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImagesTextPlacer.Strategies
{
	abstract class DefaultStrategy: IStrategy
	{
		protected string m_FileName = "";
		public string GetTextToPlace(string fileName)
		{
			m_FileName = fileName;
			if (!File.Exists(fileName))
				return "";
			return StrategyString();
		}
		public abstract string StrategyString();
	}
}
