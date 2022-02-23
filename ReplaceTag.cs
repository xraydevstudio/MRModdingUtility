using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRModdingUtility
{
    /// <summary>
    /// Replacing tags
    /// </summary>
    public class ReplaceTag
    {
        public string Name { get; }
        // Get values for replacement
        public string V1 { get; }
        // Get values to replace
        public string V2 { get; }
        // Replace tags method
        public ReplaceTag(string name, string value1, string value2) => (Name, V1, V2) = (name, value1, value2);
    }

}
