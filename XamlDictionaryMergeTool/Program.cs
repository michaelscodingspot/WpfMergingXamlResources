using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlDictionaryMergeTool
{
    class Program
    {
        class MergeParameter
        {
            public string Folder { get; set; }
            public string OutputFile { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("XamlDictionaryMergeTool: Xaml dictionary merge started");
            List<MergeParameter> mergeList = new List<MergeParameter>();
            for (int i = 0; i < args.Length / 2; i++)
            {
                string prm1 = args[i];
                string prm2 = args[i + 1];
                mergeList.Add(new MergeParameter { Folder = prm1, OutputFile = prm2 });
            }

            foreach (var mergeParams in mergeList)
            {
                Console.WriteLine(
                    string.Format("XamlDictionaryMergeTool: Merging {0} folder into a single file {1}", 
                    mergeParams.Folder, mergeParams.OutputFile));
                FolderMerger.MergeFolder(mergeParams.Folder, mergeParams.OutputFile);
            }

        }
    }
}
