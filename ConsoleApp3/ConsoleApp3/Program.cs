using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp3
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string[] videoExtensions = { ".mp4", ".mkv", ".avi", ".mov" }; // Add more video extensions if needed
            string[] subtitleExtensions = { ".srt" }; // Add more subtitle extensions if needed
            const string directoryPath = @"G:\Filmovi\Better.Call.Saul.S01-S06.COMPLETE.1080p.WEBRip.x264.EAC3-SURGE\Better.Call.Saul.S01.1080p.x264.EAC3-SURGE";
            var files = Directory.GetFiles(directoryPath);

            // Filter video files
            var videoFiles = files.Where(f => videoExtensions.Any(ext => f.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

            // Filter subtitle files
            var subtitleFiles = files.Where(f => subtitleExtensions.Any(ext => f.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

            // Rename subtitles to match video file names
            foreach (var subtitleFile in subtitleFiles)
            {
                var subtitleName = Path.GetFileNameWithoutExtension(subtitleFile);
                var subtitleExtension = Path.GetExtension(subtitleFile);

                // Extract numbers from subtitle name
                var subtitleNumbers = MyRegex().Match(subtitleName).Value;

                foreach (var newSubtitleName in from videoFile in videoFiles
                                                select Path.GetFileNameWithoutExtension(videoFile)
                                                into videoName
                                                let videoNumbers = MyRegex().Match(videoName).Value
                                                where subtitleNumbers.Equals(videoNumbers)
                                                select videoName + subtitleExtension)
                {
                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(subtitleFile) ?? throw new InvalidOperationException(), newSubtitleName)))
                    {
                        File.Move(subtitleFile, Path.Combine(Path.GetDirectoryName(subtitleFile) ?? throw new InvalidOperationException(), newSubtitleName));
                        Console.WriteLine($"Renamed {subtitleFile} to {newSubtitleName}");
                    }
                    else
                    {
                        Console.WriteLine($"Skipping renaming {subtitleFile} as {newSubtitleName} already exists.");
                    }
                }
            }
        }

        [GeneratedRegex(@"\d+")]
        private static partial Regex MyRegex();
    }

  
}
