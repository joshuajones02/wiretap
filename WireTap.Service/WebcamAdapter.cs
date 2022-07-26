namespace WireTap.Adapter
{
    public class WebcamAdapter
    {
        public void Execute(string filename) =>
            WebCam.CaptureImage(filename);
    }
}
