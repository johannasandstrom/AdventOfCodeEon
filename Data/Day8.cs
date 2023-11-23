using AdventOfCodeEon.Utility;
using System;
using System.Linq;
using System.Text;

namespace AdventOfCodeEon.Data
{
    public class Day8
    {
        private const int ScreenWidth = 50;
        private const int ScreenHeight = 6;

        private bool[,] Screen { get; set; } = new bool[ScreenHeight, ScreenWidth];

        public string PartOne(string input)
        {
            ResetScreen();
            var commands = input.SplitByNewLine();
            foreach (var command in commands)
            {
                ParseCommand(command);
            }

            return Screen.Cast<bool>().Count(b => b).ToString();
        }

        public string PartTwo(string input)
        {
            PartOne(input);
            return DrawScreen();
        }

        private void ParseCommand(string command)
        {
            var splitUpCommand = command.Split(' ');
            switch (splitUpCommand[0])
            {
                case "rect":
                    TurnOnPixels(splitUpCommand[1]);
                    break;
                case "rotate":
                    MovePixels(splitUpCommand);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void TurnOnPixels(string rect)
        {
            var size = rect.Split('x');
            for (int i = 0; i < int.Parse(size[1]); i++)
            {
                for (int j = 0; j < int.Parse(size[0]); j++)
                {
                    Screen[i, j] = true;
                }
            }
        }

        private void MovePixels(string[] directions)
        {
            var where = directions[2].Split('=');
            var howMany = int.Parse(directions[4]);

            switch (directions[1])
            {
                case "row":
                    ShiftRow(int.Parse(where[1]), howMany);
                    break;
                case "column":
                    ShiftColumn(int.Parse(where[1]), howMany);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void ShiftRow(int row, int howMany)
        {
            for (int shift = 0; shift < howMany; shift++)
            {
                var temp = Screen[row, ScreenWidth - 1];

                for (int i = ScreenWidth - 1; i > 0; i--)
                {
                    Screen[row, i] = Screen[row, i - 1];
                }
                Screen[row, 0] = temp;
            }
        }

        private void ShiftColumn(int column, int howMany)
        {
            for (int shift = 0; shift < howMany; shift++)
            {
                var temp = Screen[ScreenHeight - 1, column];

                for (int i = ScreenHeight - 1; i > 0; i--)
                {
                    Screen[i, column] = Screen[i - 1, column];
                }
                Screen[0, column] = temp;
            }
        }

        private void ResetScreen()
        {
            for (int i = 0; i < ScreenHeight; i++)
            {
                for (int j = 0; j < ScreenWidth; j++)
                {
                    Screen[i,j] = false;
                }
            }
        }

        private string DrawScreen()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < ScreenHeight; i++)
            {
                for (int j = 0; j < ScreenWidth; j++)
                {
                    sb.Append(Screen[i,j] ? "#" : "." );
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}