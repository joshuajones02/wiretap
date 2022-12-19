namespace ParentalSight.BackgroundService
{
    using System.Drawing.Imaging;

    public interface IScreenshotSettings
    {
        long DelayInMiliseconds { get; }
        string FileExtension { get; }
        string FilePath { get; }
        ImageFormat ImageFormat { get; }
        Func<string> GenerateFilename { get; }
        string GetFilePath();
    }
}