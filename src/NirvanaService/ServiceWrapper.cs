using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using EnumLogger.Extensions;
using log4net.Config;
using Newtonsoft.Json;
using NirvanaService.Configuration;

namespace NirvanaService
{
    internal class ServiceWrapper
    {
        private const string ConfigFilePath = ".\\conf\\NirvanaService.json";

        private Process _process;

        public ServiceWrapper()
        {
            InitLogging();
        }

        private void InitLogging()
        {
            var log4NetConfigPath = GetLog4NetPath();
            XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigPath));
        }

        private string GetLog4NetPath()
        {
            var assemblyInfo = new FileInfo(typeof(ServiceWrapper).Assembly.Location);
            var assemblyFolder = assemblyInfo.Directory;
            var path = Path.Combine(assemblyFolder.FullName, "log4net.config");
            return path;
        }

        public void Start()
        {
            var configs = GetConfigs();
            var serviceName = GetServiceName();
            var config = GetConfigService(configs, serviceName);
            StartProcess(config);
        }

        private void StartProcess(ServiceConfig config)
        {
            try
            {
                _process = Process.Start(new ProcessStartInfo(config.Executable, config.Options.ToString())
                {
                    UseShellExecute = false
                });
                _process.Exited += (o, e) => Stop();
                LogEvent.ServiceStarted.Log(GetType(), "Successfully started: {0}", config.Executable);
            }
            catch (Exception ex)
            {
                LogEvent.FailedToStartService.Log(GetType(),
                    "Failed to start the service with executable: {0} and arguments: {1}", config.Executable,
                    config.Options.ToString(), ex);
                throw ex;
            }
        }

        private ServiceConfig GetConfigService(Dictionary<string, ServiceConfig> configs, string serviceName)
        {
            if (configs.ContainsKey(serviceName))
            {
                return configs[serviceName];
            }
            LogEvent.MissingConfig.Log(GetType(), "Missing configuration for {0}", serviceName);
            throw new ApplicationException("Missing configuration for " + serviceName);
        }

        private string GetServiceName()
        {
            try
            {
                var processId = Process.GetCurrentProcess().Id;
                var query = "SELECT * FROM Win32_Service where ProcessId  = " + processId;
                var searcher = new ManagementObjectSearcher(query);
                string serviceName = null;
                foreach (var item in searcher.Get())
                {
                    serviceName = item["Name"].ToString();
                }
                return serviceName;
            }
            catch (Exception)
            {
                LogEvent.FailedToGetServiceName.Log(GetType(), "Failed to lookup the installed service name");
                throw;
            }
        }

        private Dictionary<string, ServiceConfig> GetConfigs()
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, ServiceConfig>>(File.ReadAllText(ConfigFilePath));
            }
            catch (Exception ex)
            {
                LogEvent.ConfigFormat.Log(GetType(), "Failed to parse config file: {0}", ConfigFilePath, ex);
                throw;
            }
        }

        public void Stop()
        {
            _process.Refresh();

            if (_process.HasExited) return;

            _process.Kill();
            _process.WaitForExit();
            _process.Dispose();
            LogEvent.ServiceStopped.Log(GetType(), "Service stopped");
        }
    }
}