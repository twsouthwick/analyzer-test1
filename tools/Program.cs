using System;
using System.Threading.Tasks;

namespace BuildAgent
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var appveyor = new AppVeyorInformation();

            if (!appveyor.IsAppveyor)
            {
                Console.Error.WriteLine("Not running on AppVeyor");
                return;
            }

            Console.WriteLine($"Branch: {appveyor.Branch}");

            var settings = await appveyor.Branch.GetConfigurationAsync();

            Console.WriteLine($"Using baseline: {settings.Baseline.Id}:{settings.Baseline.Version}");
        }
    }
}
