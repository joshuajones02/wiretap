namespace ParentalSight.BackgroundService
{
    using H.Runners;
    using Microsoft.Extensions.Hosting;
    using ParentalSight.Screenshot;

    public class Worker : BackgroundService
    {
        private readonly IScreenshotSettings _settings;

        public Worker(IScreenshotSettings settings)
        {
            _settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var runner = new ScreenshotRunner())
                {
                    using var screenshotBitmap = await runner.ShotAsync(rectangle: null, stoppingToken);
                    var filepath = _settings.GetFilePath();
                    screenshotBitmap.Save(filepath, _settings.ImageFormat);
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}