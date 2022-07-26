namespace WireTap.Adapter
{
    public class ScreenshotAdapter
    {
        public void Execute(string filename) =>
            Display.CaptureImage(filename);
    }
}
