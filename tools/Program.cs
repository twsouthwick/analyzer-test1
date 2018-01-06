using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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
            Console.WriteLine($"Comparing against: {settings.Feed}: {appveyor.Version}");

            var original = new NuGetRequestItem
            {
                Feed = settings.Baseline.Feed,
                Id = settings.Baseline.Id,
                Version = settings.Baseline.Version
            };

            var updated = new NuGetRequestItem
            {
                Feed = settings.Feed,
                Id = settings.Baseline.Id,
                Version = appveyor.Version
            };

            await SubmitAsync(original, updated);
        }

        private static async Task SubmitAsync(NuGetRequestItem original, NuGetRequestItem updated)
        {
            var request = new NuGetRequest
            {
                Original = original,
                Updated = updated
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

                var json = JsonConvert.SerializeObject(request);

                Console.WriteLine($"Request: '{json}'");

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                using (var result = await client.PostAsync($"http://52.173.34.157/api/analyzer/nuget", content))
                {
                    Console.WriteLine($"Response [{result.StatusCode}]: {await result.Content.ReadAsStringAsync()}");
                }
            }
        }
    }
}
