using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Drawing;

namespace ImagesTextPlacer
{
	class Settings
	{
		static string SETTINGS_FILENAME = "settings.xml";

		public static void SaveDefaultSettings()
		{
			XmlDocument doc = new XmlDocument();
			XmlElement root = doc.CreateElement("settings");
			doc.AppendChild(root);
			XmlNode fontSize = doc.CreateElement("font-size");
			fontSize.InnerText = "300";
			root.AppendChild(fontSize);
			XmlNode fontColor = doc.CreateElement("font-color");
			fontColor.InnerText = "LightBlue";
			root.AppendChild(fontColor);
			XmlNode side = doc.CreateElement("text-side");
			side.InnerText = "Лево";
			root.AppendChild(side);
			XmlNode position = doc.CreateElement("text-pos");
			position.InnerText = "Верх";
			root.AppendChild(position);
			XmlNode userText = doc.CreateElement("user-text");
			userText.InnerText = "";
			root.AppendChild(userText);
			XmlNode completeDateNode = doc.CreateElement("complete-date");
			completeDateNode.InnerText = "15.08.2017";
			root.AppendChild(completeDateNode);
			doc.Save(SETTINGS_FILENAME);
		}

		public static int FontSize { get; private set; }
		public static Color FontColor { get; private set; }
		public static string TextPos { get; private set; }
		public static string TextSide { get; private set; }
		public static string UserText { get; private set; }
		public static DateTime CompleteDate { get; private set; }

		public static bool ReadSettings()
		{
			if (!File.Exists(SETTINGS_FILENAME))
				return false;
			bool res = true;
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(SETTINGS_FILENAME);
				XmlNodeList userNodes = xmlDoc.SelectNodes("//settings/font-size");
				if (userNodes.Count > 0)
				{
					int fSize = 0;
					if (int.TryParse(userNodes[0].InnerText, out fSize))
						FontSize = fSize;
					else
						FontSize = 300;
				}
				userNodes = xmlDoc.SelectNodes("//settings/font-color");
				if (userNodes.Count > 0)
				{
					if (!string.IsNullOrEmpty(userNodes[0].InnerText))
						FontColor = System.Drawing.Color.FromName(userNodes[0].InnerText);
					else
						FontColor = Color.Blue;
				}
				userNodes = xmlDoc.SelectNodes("//settings/text-side");
				if (userNodes.Count > 0)
				{
					if (userNodes[0].InnerText.ToLower().Contains("лев"))
						TextSide = "Лево";
					else
						if (userNodes[0].InnerText.ToLower().Contains("прав"))
							TextSide = "Право";
						else
							return false;
				}
				userNodes = xmlDoc.SelectNodes("//settings/text-pos");
				if (userNodes.Count > 0)
				{
					if (userNodes[0].InnerText.ToLower().Contains("верх"))
						TextPos = "Верх";
					else
						if (userNodes[0].InnerText.ToLower().Contains("низ"))
							TextPos = "Низ";
						else
							return false;
				}
				userNodes = xmlDoc.SelectNodes("//settings/user-text");
				if (userNodes.Count > 0)
					UserText = userNodes[0].InnerText;
				userNodes = xmlDoc.SelectNodes("//settings/complete-date");
				if (userNodes.Count > 0)
				{
					DateTime cDate = DateTime.Now;
					if (DateTime.TryParse(userNodes[0].InnerText, out cDate))
						CompleteDate = cDate;
					else
						CompleteDate = DateTime.Now;
				}
			}
			catch {
				res = false;
			}
			return res;
		}
	}
}
