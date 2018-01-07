using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildAgent
{
    internal static class BranchExtensions
    {
        public static PackageAnalyzerSettings GetConfiguration()
        {
            var folder = Environment.GetEnvironmentVariable("APPVEYOR_BUILD_FOLDER");
            var file = Path.Combine(folder, "compat.json");
            var contents = File.ReadAllText(file);

            return JsonConvert.DeserializeObject<PackageAnalyzerSettings>(contents);
        }

        public static async Task<PackageAnalyzerSettings> GetConfigurationAsync(this BranchInfo branch)
        {
            using (var client = new HttpClient())
            {
                var uri = $"https://raw.githubusercontent.com/{branch.Name}/{branch.Commit}/compat.json";

                Console.WriteLine($"Getting config: ${uri}");

                var str = await client.GetStringAsync(uri);

                return JsonConvert.DeserializeObject<PackageAnalyzerSettings>(str);
            }
        }
    }
}
