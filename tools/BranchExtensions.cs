using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildAgent
{
    internal static class BranchExtensions
    {
        public static async Task<PackageAnalyzerSettings> GetConfigurationAsync(this BranchInfo branch)
        {
            using (var client = new HttpClient())
            {
                var uri = $"https://raw.githubusercontent.com/{branch.Name}/{branch.Commit}/compat.yml";
                var str = await client.GetStringAsync(uri);

                return JsonConvert.DeserializeObject<PackageAnalyzerSettings>(str);
            }
        }
    }
}
