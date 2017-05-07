using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesTextPlacer.Strategies
{
	interface IStrategy
	{
		string GetTextToPlace(string fileName);
	}
}
