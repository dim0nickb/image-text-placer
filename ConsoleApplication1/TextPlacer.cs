using ImagesTextPlacer.Strategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesTextPlacer
{
	class TextPlacer
	{
		public static void ProcessImages(string pathToDir){
			if (!Settings.ReadSettings())
			{
				Console.WriteLine("Не удалось загрузить файл настроек,\r\nбудут восстановлены и использованы настройки по-умолчанию.");
				Settings.SaveDefaultSettings();
				Settings.ReadSettings();
			}
			IStrategy strategy = string.IsNullOrEmpty(Settings.UserText)
					? (IStrategy)new DaysTillCompletePlaceStrategy()
					: (IStrategy)new AnyTextPlaceStrategy();
			int fontSize = Settings.FontSize;
			Color fontColor = Settings.FontColor;
			foreach (var file in Tools.GetFilesFrom(
				pathToDir,
				new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" },
				true))
			{
				using (Image inputImage = Image.FromFile(file))
				{
					string text = strategy.GetTextToPlace(file);
					Image img = GetImagWithText(inputImage, text, fontColor, fontSize);
					string dirName = Path.Combine(Path.GetDirectoryName(file), "marked");
					if (!Directory.Exists(dirName))
						Directory.CreateDirectory(dirName);
					string pathOut = Path.Combine(dirName, Path.GetFileName(file));
					img.Save(pathOut, ImageFormat.Jpeg);
				}
			}
		}

		static Image GetImagWithText(Image inputImage, string text, Color fontColor, int fontSize = 132)
		{
			using (Brush overlayTextBrush = new SolidBrush(fontColor))
			using (Font overlayTextFont = new Font(FontFamily.GenericSansSerif, fontSize))
			using (System.Drawing.Graphics graphicsInputImage = Graphics.FromImage(inputImage))
			{
				SizeF textSize = graphicsInputImage.MeasureString(text, overlayTextFont);
				Size overlayTextSize = new Size((int)textSize.Width * 2, (int)textSize.Height * 2);
				Point drawCoord = GetPosition(inputImage, (int)(textSize.Width * 1.2), (int)(textSize.Height * 1.2));
				graphicsInputImage.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				graphicsInputImage.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				graphicsInputImage.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				graphicsInputImage.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
				graphicsInputImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				graphicsInputImage.DrawString(
					text,
					overlayTextFont,
					overlayTextBrush,
					new System.Drawing.Rectangle(
						drawCoord,
						overlayTextSize)
					);
				return inputImage;
			}
		}

		private static Point GetPosition(Image inputImage, int minW, int minH)
		{
			Point res = new Point(minW, minH);
			if (Settings.TextSide.ToLower().Contains("лев"))
			{
				if (Settings.TextPos.ToLower().Contains("верх"))
				{
					res = new Point(minW, minH);
				}
				if (Settings.TextPos.ToLower().Contains("низ"))
				{
					res = new Point(minW, inputImage.Height - minH);
				}
			}
			if (Settings.TextSide.ToLower().Contains("прав"))
			{
				if (Settings.TextPos.ToLower().Contains("верх"))
				{
					res = new Point(inputImage.Width - minW, minH);
				}
				if (Settings.TextPos.ToLower().Contains("низ"))
				{
					res = new Point(inputImage.Width - minW, inputImage.Height - minH);
				}
			}
			res = new Point(res.X - minW / 2, res.Y - minH / 2);
			return res;
		}
	}
}
