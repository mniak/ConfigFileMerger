using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConfigMerge
{
    class Program
    {
        public class Options
        {
            [Value(0, Required = true, MetaName = "source", HelpText = "The source config file.")]

            public string SourcePath { get; set; }
            [Value(1, Required = true, MetaName = "target", HelpText = "The target config file.")]
            public string TargetPath { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed((errs) => HandleParseError(errs))
                ;
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            var sourcepath = Path.GetFullPath(opts.SourcePath, Directory.GetCurrentDirectory());
            var targetpath = Path.GetFullPath(opts.TargetPath, Directory.GetCurrentDirectory());

            var xml = XDocument.Load(sourcepath);

            var baseDir = Path.GetDirectoryName(sourcepath);
            Change(baseDir, xml.Root);

            File.WriteAllText(targetpath, xml.ToString());
        }

        private static void Change(string baseDir, XElement element)
        {
            var configSource = element.Attribute("configSource")?.Value;
            if (configSource != null)
            {
                var configSourceFullPath = Path.Combine(baseDir, configSource);
                if (File.Exists(configSourceFullPath))
                {
                    var replacement = XDocument.Load(configSourceFullPath).Root;
                    var dirPath = Path.GetDirectoryName(baseDir);
                    Change(dirPath, replacement);
                    element.ReplaceWith(replacement);
                }   
                return;
            }

            foreach (var subElement in element.Elements().ToList())
            {
                Change(baseDir, subElement);
            }
        }
    }
}
