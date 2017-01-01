using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XamlDictionaryMergeTool
{
    public static class XamlFileMerger
    {
        public static XElement MergeXamlFiles(XElement first, XElement second)
        {
            if (first == null || second == null)
                throw new NullReferenceException();

            XElement result = new XElement(first);
            MergeAttributes(result, second.Attributes());

            //var comments = doc.DescendantNodes().OfType<XComment>();
            MergeContent(result, second);

            return result;
        }

        private static void MergeContent(XElement result, XElement second)
        {
            var nodes = second.Nodes();
            var moreElements = second.Elements();
            var comments = second.DescendantNodes().OfType<XComment>();

            foreach (var elem in nodes)
            {
                if (elem is XElement || elem is XComment)
                    result.Add(elem);
                

            }
        }

        private static void MergeAttributes(XElement elem, IEnumerable<XAttribute> moreAttributes)
        {
            foreach (var attr in moreAttributes)
            {
                if (!elem.Attributes().Any(elemAttr => elemAttr.ToString() == attr.ToString()))
                {
                    elem.Add(new XAttribute(attr));
                }
            }
        }
    }
}
