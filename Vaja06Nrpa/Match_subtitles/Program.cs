using System.Text.RegularExpressions;

namespace Vaja06Nrpa;

class Program
{
    static void Main()
    {
        string[] videoExtensions = { ".mp4", ".mkv", ".avi", ".mov" }; // Add more video extensions if needed
        string[] subtitleExtensions = { ".srt" }; // Add more subtitle extensions if needed
        const string? directoryPath = @"G:\Filmovi\Silicon Valley Complete Series (S01 - S06) 1080p 5.1 - 2.0 x264 Phun Psyz\Season 5";
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
                     let videoNumbers = Regex.Match(videoName,
                             @"\d+")
                         .Value
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
    private static Regex MyRegex()
    {
        throw new NotImplementedException();
    }
}