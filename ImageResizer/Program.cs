using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            double total = 0;
            var count = 10;
            for (var i = 0; i < count; i++)
            {
                var sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
                var destinationPath = Path.Combine(Environment.CurrentDirectory, $"output{i+1}");
                var imageProcess = new ImageProcess();

                imageProcess.Clean(destinationPath);

                var sw1 = new Stopwatch();
                var sw2 = new Stopwatch();
                sw1.Start();
                imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
                sw1.Stop();
                imageProcess.Clean(destinationPath);

                sw2.Start();
                await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
                sw2.Stop();
                double percent = Math.Round((sw1.ElapsedMilliseconds-sw2.ElapsedMilliseconds) * 1.00 / sw1.ElapsedMilliseconds * 100.0, 4);

                Console.WriteLine($"測試第{i+1}次");
                Console.WriteLine($"花費時間(同步): {sw1.ElapsedMilliseconds} ms");
                Console.WriteLine($"花費時間(非同步): {sw2.ElapsedMilliseconds} ms");
                Console.WriteLine($"效能提升: {percent} %");

                total = total + percent;
            }
            Console.WriteLine($"平均效能提升: {total/ count} %");
            Console.ReadLine();
        }
    }
}
