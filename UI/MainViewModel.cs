using AdventOfCodeEon.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdventOfCodeEon.UI
{
    public class MainViewModel : BindableBase
    {
        private readonly Day8 _dayToSolve;
        private string? _inputText;
        private string? _outputText;

        public ICommand RunPartOneCommand { get; set; }
        public ICommand RunPartTwoCommand { get; set; }

        public string? InputText
        {
            get { return _inputText; }
            set { SetProperty(ref _inputText, value); }
        }

        public string? OutputText
        {
            get { return _outputText; }
            set { SetProperty(ref _outputText, value); }
        }

        public MainViewModel()
        {
            _dayToSolve = new Day8();
            RunPartOneCommand = new DelegateCommand(async () => await RunPart(1));
            RunPartTwoCommand = new DelegateCommand(async () => await RunPart(2));
        }

        private async Task RunPart(int part)
        {
            var input = GetInput();

            LogMessage($"===== Running Part {part} =====");

            var partTask = new Task<(string result, long elapsed)>(() => RunPart(input, part));
            partTask.Start();
            await partTask;

            LogMessage($"===== DONE (Elapsed: {partTask.Result.elapsed}ms) =====");
            LogMessage("ANSWER:");
            LogMessage(partTask.Result.result);

            Clipboard.SetText(partTask.Result.result);
        }

        private (string result, long elapsed) RunPart(string input, int part)
        {
            var start = Stopwatch.StartNew();

            string result = part == 1 ? _dayToSolve.PartOne(input) : _dayToSolve.PartTwo(input);

            var end = start.ElapsedMilliseconds;

            return (result, end);
        }

        private string GetInput()
        {
            if (InputText?.Trim().Length > 0)
            {
                return InputText;
            }

            var inputFile = Path.Combine(Environment.CurrentDirectory, "Data\\Day 8.txt");
            InputText = File.ReadAllText(inputFile);

            return InputText;
        }

        private void LogMessage(string message)
        {
            var timestamp = $"[{DateTime.Now:hh:mm:ss}] ";
            var logMsg = message.Replace("\n", $"\n{timestamp}");
            logMsg = $"{timestamp}{logMsg}";

            if (!string.IsNullOrWhiteSpace(OutputText))
            {
                logMsg = Environment.NewLine + logMsg;
            }

            OutputText += logMsg;
        }
    }
}