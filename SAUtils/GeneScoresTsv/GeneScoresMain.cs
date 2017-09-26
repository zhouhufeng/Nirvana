﻿using CommandLine.Builders;
using CommandLine.NDesk.Options;
using ErrorHandling;
using VariantAnnotation.Interface;

namespace SAUtils.GeneScoresTsv
{
    public class GeneScoresMain
    {
        private ExitCodes ProgramExecution()
        {

            return ExitCodes.Success;
        }

        public static ExitCodes Run(string command, string[] commandArgs)
        {
            var creator = new GeneScoresMain();

            var ops = new OptionSet
            {
                {
                    "in|i=",
                    "input gene scores {path}",
                    v => ConfigurationSettings.InputPath = v
                },
                {
                    "out|o=",
                    "output directory {path}",
                    v => ConfigurationSettings.OutputDirectory = v
                }
            };

            var commandLineExample = $"{command} [options]";

            var exitCode = new ConsoleAppBuilder(commandArgs, ops)
                .Parse()
                .CheckInputFilenameExists(ConfigurationSettings.InputPath, "gene scores file", "--in")
                .CheckAndCreateDirectory(ConfigurationSettings.OutputDirectory, "Output directory", "--out")
                .ShowBanner(Constants.Authors)
                .ShowHelpMenu("Reads provided OMIM data files and populates tsv file", commandLineExample)
                .ShowErrors()
                .Execute(creator.ProgramExecution);

            return exitCode;
        }
    }
}