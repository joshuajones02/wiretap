namespace ParentalSight.BackgroundService
{
    using System.Drawing.Imaging;

    public class ScreenshotSettings : IScreenshotSettings
    {
        public ScreenshotSettings(long delayInMiliseconds, Func<string> generateFilename, string filePath, string fileExtension = ".PNG", ImageFormat? imageFormat = null)
        {
            DelayInMiliseconds = delayInMiliseconds;
            GenerateFilename = generateFilename;
            FilePath = filePath;
            FileExtension = fileExtension;
            ImageFormat = imageFormat ?? ImageFormat.Png;
        }
        
        public long DelayInMiliseconds { get; }

        public string FileExtension { get; }

        public string FilePath { get; }

        public ImageFormat ImageFormat { get; }

        public Func<string> GenerateFilename { get; }

        public string GetFilePath()
        {
            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);

            var filename = GenerateFilename() + FileExtension;
            var filepath = Path.Combine(FilePath, filename);

            return filepath;
        }
    }
}