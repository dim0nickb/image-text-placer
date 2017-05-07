using System;

namespace ImagesTextPlacer
{
	class Program
	{

		static void Main(string[] args)
		{
			//Settings.SaveDefaultSettings();
			//return;
			try
			{
				TextPlacer.ProcessImages(Environment.CurrentDirectory);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error: {0}\r\nStack:\r\n{1}", ex.Message, ex.StackTrace);
				Console.ReadKey();
			}
			Console.WriteLine("Complete");
		}

	}
}
