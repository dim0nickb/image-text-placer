using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesTextPlacer.Strategies
{
	class AnyTextPlaceStrategy : DefaultStrategy
	{
		public override string StrategyString()
		{
			string res = Settings.UserText;
			return res;
		}
	}
}
