namespace ParentalSight.Screenshot
{
    using ParentalSight;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    internal class Service : IScreen, IParentalService
    {
        public void CaptureImage(string filepath) => InternalCapture(filepath);

        public void Capture(string filepath) => InternalCapture(filepath);

        protected void InternalCapture(string filepath)
        {
            Console.WriteLine("Capturing screenshot...");
            var screen = SystemInformation.VirtualScreen;
            using var bmp = new Bitmap(screen.Width, screen.Height);
            using (var graphic = Graphics.FromImage(bmp))
            {
                graphic.CopyFromScreen(screen.Left, screen.Top, 0, 0, bmp.Size);
            }

            bmp.Save(filepath, ImageFormat.Jpeg);
        }
    }
}