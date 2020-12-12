using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace TerrainGen.Resources
{
    public static class ResourceManager
    {
        public static Dictionary<ushort, XElement> _tilesXML;

        public static void Initializa()
        {
            _tilesXML = new Dictionary<ushort, XElement>();

            var directory = Directory.EnumerateFiles("Resources/", "*xml");

            foreach(var s in directory)
            {
                var xmlFile = XElement.Parse(File.ReadAllText(s));

                foreach (var e in xmlFile.Elements("Ground"))
                    _tilesXML.Add(e.ParseUshort("@type"), e);
            }
        }

        public static ushort ParseUshort(this XElement element, string name, ushort undefined = 0)
        {
            string value = name[0].Equals('@') ? element.Attribute(name.Remove(0, 1))?.Value : element.Element(name)?.Value;
            if (string.IsNullOrWhiteSpace(value)) return undefined;
            return (ushort)(value.StartsWith("0x") ? Int32.Parse(value.Substring(2), NumberStyles.HexNumber) : Int32.Parse(value));
        }
    }
}
