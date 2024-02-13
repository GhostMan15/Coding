using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] videoExtensions = { ".mp4", ".mkv", ".avi", ".mov" }; // Add more video extensions if needed
        string[] subtitleExtensions = { ".srt" }; // Add more subtitle extensions if needed
        string directoryPath = @"G:\Filmovi\Silicon Valley Complete Series (S01 - S06) 1080p 5.1 - 2.0 x264 Phun Psyz\Season 5";
        string[] files = Directory.GetFiles(directoryPath);

        // Filter video files
        var videoFiles = files.Where(f => videoExtensions.Any(ext => f.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

        // Filter subtitle files
        var subtitleFiles = files.Where(f => subtitleExtensions.Any(ext => f.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();

        // Rename subtitles to match video file names
        foreach (var subtitleFile in subtitleFiles)
        {
            string subtitleName = Path.GetFileNameWithoutExtension(subtitleFile);
            string subtitleExtension = Path.GetExtension(subtitleFile);

            // Extract numbers from subtitle name
            var subtitleNumbers = Regex.Match(subtitleName, @"\d+").Value;

            foreach (var videoFile in videoFiles)
            {
                string videoName = Path.GetFileNameWithoutExtension(videoFile);

                // Extract numbers from video name
                var videoNumbers = Regex.Match(videoName, @"\d+").Value;

                // Compare numbers in names
                if (subtitleNumbers.Equals(videoNumbers))
                {
                    string newSubtitleName = videoName + subtitleExtension; // Construct new subtitle file name
                    if (!File.Exists(Path.Combine(Path.GetDirectoryName(subtitleFile), newSubtitleName)))
                    {
                        File.Move(subtitleFile, Path.Combine(Path.GetDirectoryName(subtitleFile), newSubtitleName));
                        Console.WriteLine($"Renamed {subtitleFile} to {newSubtitleName}");
                    }
                    else
                    {
                        Console.WriteLine($"Skipping renaming {subtitleFile} as {newSubtitleName} already exists.");
                    }
                }
            }
        }
    }
}
