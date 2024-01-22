using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Xml;
using System.Xml.Serialization;

namespace ExordiumGames.MVC.Utils.Parsers
{
    public class XMLToJsonParser
    {
        public string Convert(string xml)
        {
            var settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.LineNumberOffset = 2;

            var reader = XmlReader.Create(xml, settings);
            string Content = new("");

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        // The node is an element.
                        Content += $"<{reader.Name}";
                        while (reader.MoveToNextAttribute()) // Read the attributes.
                            Content += " " + reader.Name + "='" + reader.Value + "'";
                        Content += ">";
                        break;
                    case XmlNodeType.CDATA:
                        Content += reader.Value;
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Content += $"{reader.Value}";
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Content += "</" + reader.Name;
                        Content += ">";
                        break;

                }
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Content);
            string json = JsonConvert.SerializeXmlNode(doc);
            return json;
        }
    }
}
