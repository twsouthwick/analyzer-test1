using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildAgent
{

    public class PackageAnalyzerSettings
    {
        public BaselinePackage Baseline { get; set; }
    }
}
