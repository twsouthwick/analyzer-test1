using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildAgent
{

    public class BaselinePackage
    {
        public string Feed { get; set; }

        public string Id { get; set; }

        public string Version { get; set; }
    }
}
