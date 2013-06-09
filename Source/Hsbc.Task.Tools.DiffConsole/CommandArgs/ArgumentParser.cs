using System;
using System.Diagnostics;
using System.IO;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Mono.Options;

namespace Hsbc.Task.Tools.DiffConsole.CommandArgs
{
    public class ArgumentParser
    {
        private readonly OptionSet _optionSet;
        private readonly ArgumentValidator _argumentValidator;
        private readonly DiffOptions _diffOptions;
        private bool _showHelp;

        public ArgumentParser()
        {
            _diffOptions = new DiffOptions();
            _optionSet = CreateOptionSet();
            _argumentValidator = new ArgumentValidator();
        }

        public OptionSet CreateOptionSet()
        {
            var optionSet = new OptionSet()
                        {
                            {
                                "sa|showall", "Show all contents (including no changed ones).",
                                option => _diffOptions.ShouldShowAll= option!=null
                                },
                            {
                                "pl|perline",
                                "Show per line output, showing the differences to reach target file.",
                                option => _diffOptions.ShouldShowPerLine=option!=null
                                },                   
                            {
                                "h|help", "show this message and exit",
                                option => _showHelp = option != null
                                },
                        };
            return optionSet;
        }

        public bool TryParseArgs(string[] args, out DiffOptions result)
        {
            result = _diffOptions;
            try
            {
                _optionSet.Parse(args);
                if(_showHelp)
                {
                    var helpText = File.ReadAllLines(@".\Resources\HelpText.txt");
                    Console.WriteLine(string.Join(Environment.NewLine,helpText));
                    return false;
                }
                Debug.WriteLine(_diffOptions);
                bool areArgumentsValid = _argumentValidator.AreValid(args);
                if(areArgumentsValid)
                {
                    _diffOptions.SourceFile = args[0];
                    _diffOptions.TargetFile = args[1];
                }
                return areArgumentsValid;
            }
            catch (OptionException e)
            { 
                Console.Write("diffTool : ");
                Console.WriteLine(e.Message);
                Console.WriteLine("diffTool --help for more information.");
                return false;
            }
        }


        private class ArgumentValidator
        {
            public bool AreValid(string[] args)
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Invalid number of arguments, expecting source and target file. Refer help");
                    return false;
                }

                var firstFile = args[0];
                if (!File.Exists(firstFile))
                {
                    Console.WriteLine("Invalid argument for source file. First argument is expected to be source file. File :{0} is not found in current directory.", firstFile);
                    return false;
                }

                var secondFile = args[1];
                if (!File.Exists(secondFile))
                {
                    Console.WriteLine("Invalid argument for target file. Second argument is expected to be target file. File :{0} is not found in current directory.", secondFile);
                    return false;
                }

                return true;
            }

        }
    }


}
