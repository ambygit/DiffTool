using System;
using Hsbc.Task.Tools.DiffConsole.CommandArgs;
using Hsbc.Task.Tools.DiffLibrary.Engine.Main;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;

namespace Hsbc.Task.Tools.DiffConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DiffOptions optionValue;
            var argumentParser = new ArgumentParser();
            if (argumentParser.TryParseArgs(args, out optionValue))
            {
                var diffCalculatorEngine =
                    new DiffCalculator(DiffResultOptionFactory.Instance.Create(optionValue));
                var result = diffCalculatorEngine.Run(optionValue.SourceFile, optionValue.TargetFile);
                result.Print(Console.WriteLine);
            }
        }
    }
}