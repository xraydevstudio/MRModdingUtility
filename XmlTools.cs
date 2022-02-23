using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MRModdingUtility
{
    public class XmlTools
    {
        /// <summary>
        /// Replace some tags in Motor Rock map xml file
        /// </summary>
        /// <param name="docName"></param>
        /// <returns></returns>
        public async Task ReplaceTags(string docName, params ReplaceTag[] replaceTags)
        {
            string map = File.ReadAllText(docName);
            foreach (var tag in replaceTags)
            {
                if (map.Contains(tag.V2))
                {
                    map = map.Replace(tag.V2, tag.V1);
                }
                else if (map.Contains(tag.V1))
                {
                    map = map.Replace(tag.V1, tag.V2);
                }
            }
            await File.WriteAllTextAsync(docName, map);
        }
        /// <summary>
        /// Read position values from map file
        /// </summary>
        /// <param name="docName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PositionNode>>? ReadPositionNodes(string docName)
        {
            // Load xml document
            XmlDocument doc = new();
            doc.Load(docName);

            // Select nodes with pos tag
            XmlNodeList posList = doc.SelectNodes("//pos");
            if (!(posList is { Count: > 0 })) return default;
            // Get pos values
            var nodes = new List<PositionNode>();
            for (int i = 0; i < posList.Count; i++)
            {
                XmlNode xn = posList[i];
                if (!xn.HasChildNodes) return default;
                for (int j = 0; j < xn.ChildNodes.Count; j++)
                {
                    XmlNode item = xn.ChildNodes[j];
                    string[] values = item.InnerText.Split(' ');
                    var (x, y, z) = (float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat));
                    PositionNode node = new PositionNode(x, y, z, (i, j));
                    nodes.Add(node);
                }
            }
            return nodes;
        }
        /// <summary>
        /// Write new position values to map file
        /// </summary>
        /// <param name="docName"></param>
        /// <param name="nodes"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public async Task<bool> UpdateNodesPosition(string docName, IEnumerable<PositionNode> nodes, float x = 0, float y = 0, float z = 0)
        {
            try
            {
                foreach (var node in nodes)
                {
                    node.X += x;
                    node.Y += y;
                    node.Z += z;
                }
                // Load xml document
                XmlDocument doc = new();
                doc.Load(docName);

                // Select nodes with pos tag
                XmlNodeList posList = doc.SelectNodes("//pos");
                if (!(posList is { Count: > 0 })) return default;
                // Get pos values
                for (int i = 0; i < posList.Count; i++)
                {
                    XmlNode xn = posList[i];
                    if (!xn.HasChildNodes) return default;
                    for (int j = 0; j < xn.ChildNodes.Count; j++)
                    {
                        XmlNode item = xn.ChildNodes[j];
                        var node = nodes.FirstOrDefault(node => node.IterPosition.i == i && node.IterPosition.j == j);
                        item.InnerText = node.ToXml;
                    }
                }
                doc.Save(docName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
