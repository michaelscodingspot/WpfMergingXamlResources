using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XamlDictionaryMergeTool;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace MergeToolTests
{
    [TestClass]
    [DeploymentItem("Resources\\")]
    public class MergerTests
    {
        const string FONT_RESOURCES = "FontResources.txaml";
        const string COLOR_RESOURCES = "ColorResources.txaml";

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Merger_RunningMergeOnNull_ExceptionIsThrown()
        {
            XamlFileMerger.MergeXamlFiles(null, null);


        }

        [TestMethod]
        public void Merger_RunningMergeOnFontAndColor_NoException()
        {
            XamlFileMerger.MergeXamlFiles(GetFontResource(), GetColorResource());
        }

        [TestMethod]
        public void Merger_RunningMergeOnFontAndColor_SystemAndXNamespacesExists()
        {
            var res = XamlFileMerger.MergeXamlFiles(GetFontResource(), GetColorResource());
            IEnumerable<XAttribute> attributes = res.Attributes();
            var xmlns = attributes.First(attr => attr.Name == "xmlns");
            var x = attributes.First(attr => attr.Name.LocalName == "x");
            var system = attributes.First(attr => attr.Name.LocalName == "system");
            var collections = attributes.First(attr => attr.Name.LocalName == "collections");
            Assert.AreEqual("http://schemas.microsoft.com/winfx/2006/xaml/presentation", xmlns.Value);
            Assert.AreEqual("http://schemas.microsoft.com/winfx/2006/xaml", x.Value);
            Assert.AreEqual("clr-namespace:System;assembly=mscorlib", system.Value);
            Assert.AreEqual("clr-namespace:System.Collections;assembly=mscorlib", collections.Value);
        }

        [TestMethod]
        public void Merger_RunningMergeOnFontAndColor_AllelementsIncluded()
        {
            var res = XamlFileMerger.MergeXamlFiles(GetFontResource(), GetColorResource());
            IEnumerable<XElement> elements = res.Elements();

            var brushRed = GetAttributeWithKey(elements, "Brush.Red");
            var fontSizeMedium = GetAttributeWithKey(elements, "FontSize.Medium");
            var fontSizeLarge = GetAttributeWithKey(elements, "FontSize.Large");

            var elementsAsList = elements.ToList();
            int brushRedIndex = elementsAsList.IndexOf(brushRed);
            int fontSizeMediumIndex = elementsAsList.IndexOf(fontSizeMedium);
            int fontSizeLargeIndex = elementsAsList.IndexOf(fontSizeLarge);

            Assert.IsTrue(fontSizeLargeIndex > fontSizeMediumIndex);
            Assert.IsTrue(brushRedIndex > fontSizeLargeIndex);

        }

        private static XElement GetAttributeWithKey(IEnumerable<XElement> elements, string key)
        {
            return elements.First(elem => elem.Attributes().
                Any(attr => !String.IsNullOrEmpty(attr.Name.LocalName)
                && attr.Name.LocalName == "Key"
                && attr.Value == key));
        }

        private static XElement GetColorResource()
        {
            var doc = XDocument.Load(COLOR_RESOURCES);
            var elem = doc.Root;
            return elem;
        }

        private static XElement GetFontResource()
        {
            var doc = XDocument.Load(FONT_RESOURCES);
            var elem = doc.Root;
            return elem;
        }
    }
}
