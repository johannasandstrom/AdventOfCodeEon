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
                    TurnOnPixelsInUpperCorner(splitUpCommand[1]);
                    break;
                case "rotate":
                    ShiftPixels(splitUpCommand);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void TurnOnPixelsInUpperCorner(string rect)
        {
            var size = rect.Split('x');
            var height = int.Parse(size[1]);
            var width = int.Parse(size[0]);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width ; j++)
                {
                    Screen[i, j] = true;
                }
            }
        }

        // Format of string 'shiftCommand': rotate row y=A by B / rotate column x=A by B
        private void ShiftPixels(string[] shiftCommand)
        {
            var where = shiftCommand[2].Split('=');
            var arrayNumber = int.Parse(where[1]);
            var numberOfSteps = int.Parse(shiftCommand[4]);

            switch (shiftCommand[1])
            {
                case "row":
                    ShiftRow(arrayNumber, numberOfSteps);
                    break;
                case "column":
                    ShiftColumn(arrayNumber, numberOfSteps);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void ShiftRow(int rowNumber, int numberOfSteps)
        {
            for (int shift = 0; shift < numberOfSteps; shift++)
            {
                var temp = Screen[rowNumber, ScreenWidth - 1];

                for (int i = ScreenWidth - 1; i > 0; i--)
                {
                    Screen[rowNumber, i] = Screen[rowNumber, i - 1];
                }

                Screen[rowNumber, 0] = temp;
            }
        }

        private void ShiftColumn(int columnNumber, int numberOfSteps)
        {
            for (int shift = 0; shift < numberOfSteps; shift++)
            {
                var temp = Screen[ScreenHeight - 1, columnNumber];

                for (int i = ScreenHeight - 1; i > 0; i--)
                {
                    Screen[i, columnNumber] = Screen[i - 1, columnNumber];
                }

                Screen[0, columnNumber] = temp;
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