using System;
using System.IO;
using System.Collections.Generic;
using NDesk.Options;
using Collage.Engine;

namespace CollageConsole
{
    // TODO: Refactor (create separate classes, rename, clean-up)

    class Program
    {
        const string programName = "collage";

        static bool showHelp = false;
        static string inputDirectory = "", outputDirectory = "";
        static int tileHeight = 5, tileWidth = 5;
        static int numberOfRows = 5, numberOfColums = 5;
        static bool rotateAndFlipRandomly = false;
        static bool convertToGrayscale = false;
        static int scalePercent = 50;

        static bool tileHeightParsed = true, tileWidthParsed = true,
            numberOfRowsParsed = true, numberOfColumnsParsed = true,
            scalePercentParsed = true;

        static void Main(string[] args)
        {
            var options = CreateOptions();

            List<string> extra;
            try
            {
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("{0}: ", programName);
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `{0} --help' for more information.", programName);
                return;
            }

            if (showHelp)
            {
                ShowHelp(options);
                return;
            }

            if (!ValidateOptions())
                return;

            var imagesList = new List<string>();
            imagesList.AddRange(Directory.GetFiles(inputDirectory, "*.jpg"));

            if (imagesList.Count == 0)
            {
                var dirInfo = new DirectoryInfo(inputDirectory);
                Console.WriteLine("No images found in {0}.", dirInfo.FullName);
                return;
            }
            else
            {
                Console.WriteLine("Number of images found: {0}", imagesList.Count);
            }

            var collageSettings = new CollageSettings
            {
                InputImages = imagesList,
                NumberOfColumns = numberOfColums,
                NumberOfRows = numberOfRows,
                TileHeight = tileHeight,
                TileWidth = tileWidth,
                RotateAndFlipRandomly = rotateAndFlipRandomly,
                ConvertToGrayscale = convertToGrayscale,
                OutputDirectory = outputDirectory,
                ScalePercent = scalePercent
            };

            var collage = new CollageEngine(collageSettings);

            //collage.ProgressChanged += new CollageEngine.CollageProgressChangedEventHandler(collage_ProgressChanged);

            string fileName = collage.Create();

            Console.WriteLine("Collage saved: {0}", Path.Combine(outputDirectory, fileName));
        }

        static void collage_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Console.Write("\r{0}", e.ProgressPercentage);
        }

        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: {0} [OPTIONS]", programName);
            Console.WriteLine("Creates a very nice collage out of specified input images.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }

        static OptionSet CreateOptions()
        {
            var options = new OptionSet
            {
                { 
                    "i|input=", 
                    "the pictures {INPUT DIRECTORY}. Default is application directory.",
                    x => inputDirectory = x
                },
                {
                    "o|output=",
                    "the {OUTPUT DIRECTORY} in which collage image will be saved. Default is application directory.",
                    x => outputDirectory = x
                },
                {
                    "th|height=",
                    "single {TILE HEIGHT} in pixels. Default is 5.",
                    x => 
                    {
                        int temp;
                        if (int.TryParse(x, out temp) && temp > 0)
                            tileHeight = temp;
                        else
                            tileHeightParsed = false;
                    }
                },
                {
                    "tw|width=",
                    "single {TILE WIDTH} in pixels. Default is 5.",
                    x =>
                    {
                        int temp;
                        if (int.TryParse(x, out temp) && temp > 0)
                            tileWidth = temp;
                        else
                            tileWidthParsed = false;
                    }
                },
                {
                    "r|rows=",
                    "number of {ROWS}. Default is 5",
                    x =>
                    {
                        int temp;
                        if (int.TryParse(x, out temp) && temp > 0)
                            numberOfRows = temp;
                        else
                            numberOfRowsParsed = false;
                    }
                },
                {
                    "c|columns=",
                    "number of {COLUMNS}. Default is 5.",
                    x =>
                    {
                        int temp;
                        if (int.TryParse(x, out temp) && temp > 0)
                            numberOfColums = temp;
                        else
                            numberOfColumnsParsed = false;
                    }
                },
                {
                    "rf|RotateFlip",
                    "rotate and flip collage tiles. Default is false.",
                    x => rotateAndFlipRandomly = x != null
                },
                {
                    "g|grayscale",
                    "convert to grayscale.",
                    x => convertToGrayscale = x != null
                },
                {
                    "s|scale=",
                    "scale percent [0-100]% of every input image. Default is 50%.",
                    x =>
                    {
                        int temp;
                        if (int.TryParse(x, out temp) && temp > 0 && temp <= 100)
                            scalePercent = temp;
                        else
                            scalePercentParsed = false;
                    }
                },
                { 
                    "h|help",  
                    "show this message and exit.", 
                    x => showHelp = x != null 
                }
            };
            return options;
        }

        static bool ValidateOptions()
        {
            bool result = true;

            if (!tileWidthParsed)
            {
                ShowValidationError("Tile width must be a positive integer.");
                result = false;
            }
            if (!tileHeightParsed)
            {
                ShowValidationError("Tile height must be a positive integer.");
                result = false;
            }
            if (!numberOfColumnsParsed)
            {
                ShowValidationError("Number of rows must be a positive integer.");
                result = false;
            }
            if (!numberOfRowsParsed)
            {
                ShowValidationError("Number of columns must be a positive integer.");
                result = false;
            }
            if (!scalePercentParsed)
            {
                ShowValidationError("Scale percent must be an integer betweeen 0 and 100.");
                result = false;
            }
            if (!Directory.Exists(inputDirectory))
            {
                ShowValidationError("Input directory does not exist.");
                result = false;
            }
            if (!Directory.Exists(outputDirectory))
            {
                ShowValidationError("Output directory does not exist.");
                result = false;
            }
            if (!tileHeightParsed)
            {
                ShowValidationError("Tile height cannot be parsed.");
                result = false;
            }
            if (!tileWidthParsed)
            {
                ShowValidationError("Tile width cannot be parsed.");
                result = false;
            }
            if (!numberOfColumnsParsed)
            {
                ShowValidationError("Number of columns cannot be parsed.");
                result = false;
            }
            if (!numberOfRowsParsed)
            {
                ShowValidationError("Number of rows cannot be parsed.");
                result = false;
            }
            if (!scalePercentParsed)
            {
                ShowValidationError("Scale percent cannot be parsed.");
                result = false;
            }
            return result;
        }

        static void ShowValidationError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR: {0}", message);
            Console.ResetColor();
        }
    }
}
