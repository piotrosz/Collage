using System;
using Collage.Engine;
using System.Collections.Generic;
using NDesk.Options;
using System.IO;

namespace CollageConsole
{
    class Program
    {
        const string programName = "collage";

        static void Main(string[] args)
        {
            bool showHelp = false;
            string inputDirectory = "", outputDirectory = "";
            int tileHeight = 5, tileWidth = 5;
            int numberOfRows = 5, numberOfColums = 5;
            bool rotateAndFlipRandomly = false;
            bool convertToGrayscale = false;

            bool tileHeightParsed = true, tileWidthParsed = true,
                numberOfRowsParsed = true, numberOfColumnsParsed = true;
            bool inputDirectoryExists = true, outputDirectoryExists = true;

            #region OptionSet
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
                    "h|help",  
                    "show this message and exit", 
                    x => showHelp = x != null 
                }
            };
            #endregion

            #region Validation and help message
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

            if (!tileWidthParsed)
                Console.WriteLine("Tile width must be a positive integer.");
            if (!tileHeightParsed)
                Console.WriteLine("Tile height must be a positive integer.");
            if (!numberOfColumnsParsed)
                Console.WriteLine("Number of rows must be a positive integer.");
            if (!numberOfRowsParsed)
                Console.WriteLine("Number of columns must be a positive integer.");
            if (!Directory.Exists(inputDirectory))
            {
                inputDirectoryExists = false;
                Console.WriteLine("Input directory does not exist.");
            }
            if (!Directory.Exists(outputDirectory))
            {
                outputDirectoryExists = false;
                Console.Write("Output directory does not exist.");
            }
            #endregion

            if (tileHeightParsed && tileWidthParsed
                && numberOfColumnsParsed && numberOfRowsParsed
                && inputDirectoryExists && outputDirectoryExists)
            {
                var imagesList = new List<string>();
                imagesList.AddRange(Directory.GetFiles(inputDirectory, "*.jpg"));

                if (imagesList.Count == 0)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(inputDirectory);
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
                    OutputDirectory = outputDirectory
                };

                var collage = new CollageEngine(collageSettings);
                string fileName = collage.CreateCollage();

                Console.WriteLine("Collage saved: {0}", Path.Combine(outputDirectory, fileName));
            }
        }

        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: {0} [OPTIONS]", programName);
            Console.WriteLine("Creates a very nice collage out of specified input images.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
