using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRModdingUtility
{
    /// <summary>
    /// Split read position inner-text to X, Y, Z;
    /// format them back to original format.
    /// </summary>
    public class PositionNode
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public (int i, int j) IterPosition { get; }

        /// <summary>
        /// Split string values to XYZ method
        /// </summary>
        /// <param name="x">Object positions X</param>
        /// <param name="y">Object positions Y</param>
        /// <param name="z">Object positions Z</param>
        /// <param name="xmlPosition">Particular pos node in xml-file</param>
        public PositionNode(float x, float y, float z, (int, int) xmlPosition)
        {
            X = x;
            Y = y;
            Z = z;
            IterPosition = xmlPosition;
        }
        /// <summary>
        /// Format updated values by user to xml map file
        /// </summary>
        public string ToXml
        {
            get
            {
                return $"{X} {Y} {Z}".Replace(',', '.');
            }
        }
        // For testing
        // Uncomment if you want to return all XYZ pos from xml in console
        /* public override string ToString()
        {
            return $"x: {X}, y: {Y}, z: {Z}";
        } */
    }
}
