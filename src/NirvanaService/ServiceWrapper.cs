using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using Newtonsoft.Json;

namespace NirvanaService
{
    internal class ServiceWrapper
    {
        private string ConfigFilePath = ".\\conf\\NirvanaService.json";

        private readonly string _name;
        private string _a;

        public ServiceWrapper(string name, string a = "")
        {
            _name = name;
            _a = a;
        }

        public void Start()
        {
            try
            {
                var configs =
                    JsonConvert.DeserializeObject<Dictionary<string, ServiceConfig>>(File.ReadAllText(ConfigFilePath));


                var processId = Process.GetCurrentProcess().Id;


                var query = "SELECT * FROM Win32_Service where ProcessId  = " + processId;
                var searcher = new ManagementObjectSearcher(query);
                foreach (var item in searcher.Get())
                {
                    _a = item["Name"].ToString();
                }
                if (configs.ContainsKey(_a))
                {
                    var config = configs[_a];
                    var process = Process.Start(new ProcessStartInfo(config.Executable, config.Options.ToString())
                    {
                        UseShellExecute = false
                    });
                    process.Exited += (o, e) => Stop();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("c:\\tmp\\log.txt", ex.ToString());

                throw;
            }
        }

        public void Stop()
        {
            

        }
    }

    public class ServiceConfig
    {
        public string Executable { get; set; }
        public ServiceOptions Options { get; set; }
    }

    public class ServiceOptions
    {
        public string ArgSeparator { get; set; }
        public string ArgPrefix { get; set; }
        public Dictionary<string, object> Arguments { get; set; }

        public override string ToString()
        {
            return Arguments.Aggregate("",
                (aggr, next) => string.Format("{0} {1}{2}{3}{4}", aggr, ArgPrefix, next.Key, ArgSeparator, next.Value));
        }
    }
}