using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRModdingUtility
{
    public class TagService
    {
        private IDictionary<string, ReplaceTag> _tagsDict;
        public TagService(string txtFileLocation)
        {
            _tagsDict = new Dictionary<string, ReplaceTag>();
            var text = File.ReadAllText(txtFileLocation);
            foreach (var values in text.Split(';'))
            {
                var splitted = values.Split(',');
                var(name, v1, v2) = (splitted[0], splitted[1], splitted[2]);
                ReplaceTag tag = new ReplaceTag(name, v1, v2);
                _tagsDict.Add(tag.Name, tag);
            }
        }
        public ReplaceTag this[string tagName] => _tagsDict[tagName];
    }
}
