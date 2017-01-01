using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XamlDictionaryMergeTool
{
    public static class FolderMerger
    {
        public static void MergeFolder(string folder, string outFile)
        {
            var files = GetXamlFilesRecurssive(folder, outFile);
            XElement merged = MergeFiles(files);
            WriteXElementToFile(outFile, merged);
        }

        

        private static XElement MergeFiles(List<string> files)
        {
            var filesAsXelement = files.Select(file =>
            {
                var root = XDocument.Load(file).Root;
                root.AddFirst(new XComment(string.Format("Merged from file {0}", Path.GetFileName(file))));
                root.AddFirst(new XComment(""));
                return root;
            });

            XElement merged = filesAsXelement.Aggregate((first, second) =>
            {
                
                var mergeRes = XamlFileMerger.MergeXamlFiles(first, second);
                return mergeRes;
            });

            merged.AddFirst(new XComment(""));
            merged.AddFirst(new XComment("This is an auto-generated file. Do not change by yourself since it's overridden with each build"));
            merged.Add(new XComment("This XAML merge infrastructure was provided by http://michaelscodingspot.com/"));
            return merged;
        }

        private static List<string> GetXamlFilesRecurssive(string folder, string excludeFile)
        {
            List<string> res = new List<string>();
            var subfolders = Directory.GetDirectories(folder).OrderBy(el => el);

            foreach (var subfolder in subfolders)
            {
                res.AddRange(GetXamlFilesRecurssive(subfolder, excludeFile));
            }

            var xamlFiles = Directory.GetFiles(folder, "*", SearchOption.TopDirectoryOnly)
                .Where(file =>
                {
                    if (file == excludeFile)
                        return false;
                    var extension = new FileInfo(file).Extension.ToLower();
                    return extension == ".xaml" || extension == ".txaml";
                });
            res.AddRange(xamlFiles);

            return res;
        }

        private static void WriteXElementToFile(string outFile, XElement merged)
        {
            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                fileInfo.IsReadOnly = false;
            }

            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates};
            using (XmlWriter xw = XmlWriter.Create(outFile, xws))
                merged.Save(xw);
        }
    }
}
