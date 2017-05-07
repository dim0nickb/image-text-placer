using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesTextPlacer.Strategies
{
	class DaysTillCompletePlaceStrategy: DefaultStrategy
	{
		public override string StrategyString()
		{
			DateTime creationTime = File.GetCreationTime(m_FileName);

			string result = ((Settings.CompleteDate - creationTime).Days + 1).ToString();
			return result;
		}
	}
}
