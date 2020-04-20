using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class CustomChromeDriver : RemoteWebDriver
    {
        private const string GetNetworkConditionsCommand = "getNetworkConditions";
        private const string SetNetworkConditionsCommand = "setNetworkConditions";
        private const string DeleteNetworkConditionsCommand = "deleteNetworkConditions";
        private const string SendChromeCommand = "sendChromeCommand";
        private const string SendChromeCommandWithResult = "sendChromeCommandWithResult";

        private ChromeDriverService _driverService;

        public CustomChromeDriver(Uri remoteAddress, DriverOptions options, ChromeDriverService driverService)
        : base(remoteAddress, options)
        {
            this.AddCustomChromeCommand(GetNetworkConditionsCommand, CommandInfo.GetCommand, "/session/{sessionId}/chromium/network_conditions");
            this.AddCustomChromeCommand(SetNetworkConditionsCommand, CommandInfo.PostCommand, "/session/{sessionId}/chromium/network_conditions");
            this.AddCustomChromeCommand(DeleteNetworkConditionsCommand, CommandInfo.DeleteCommand, "/session/{sessionId}/chromium/network_conditions");
            this.AddCustomChromeCommand(SendChromeCommand, CommandInfo.PostCommand, "/session/{sessionId}/chromium/send_command");
            this.AddCustomChromeCommand(SendChromeCommandWithResult, CommandInfo.PostCommand, "/session/{sessionId}/chromium/send_command_and_get_result");

            this._driverService = driverService;
        }

        public ChromeNetworkConditions NetworkConditions
        {
            get
            {
                Response response = this.Execute(GetNetworkConditionsCommand, null);
                return FromDictionary(response.Value as Dictionary<string, object>);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "value must not be null");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters["network_conditions"] = ToDictionary(value);
                this.Execute(SetNetworkConditionsCommand, parameters);
            }
        }

        public IWebStorage CustomStorage
        {
            get
            {
                return new RemoteWebStorage(this);
            }
        }

        static internal ChromeNetworkConditions FromDictionary(Dictionary<string, object> dictionary)
        {
            ChromeNetworkConditions conditions = new ChromeNetworkConditions();
            if (dictionary.ContainsKey("offline"))
            {
                conditions.IsOffline = (bool)dictionary["offline"];
            }

            if (dictionary.ContainsKey("latency"))
            {
                conditions.Latency = TimeSpan.FromMilliseconds(Convert.ToDouble(dictionary["latency"]));
            }

            if (dictionary.ContainsKey("upload_throughput"))
            {
                conditions.UploadThroughput = (long)dictionary["upload_throughput"];
            }

            if (dictionary.ContainsKey("download_throughput"))
            {
                conditions.DownloadThroughput = (long)dictionary["download_throughput"];
            }

            return conditions;
        }

        internal Dictionary<string, object> ToDictionary(ChromeNetworkConditions network)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary["offline"] = network.IsOffline;
            if (network.Latency != TimeSpan.Zero)
            {
                dictionary["latency"] = Convert.ToInt64(network.Latency.TotalMilliseconds);
            }

            if (network.DownloadThroughput >= 0)
            {
                dictionary["download_throughput"] = network.DownloadThroughput;
            }

            if (network.UploadThroughput >= 0)
            {
                dictionary["upload_throughput"] = network.UploadThroughput;
            }

            return dictionary;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this._driverService != null)
            {
                this._driverService.Dispose();
                this._driverService = null;
            }
        }

        private void AddCustomChromeCommand(string commandName, string method, string resourcePath)
        {
            CommandInfo commandInfoToAdd = new CommandInfo(method, resourcePath);
            this.CommandExecutor.CommandInfoRepository.TryAddCommand(commandName, commandInfoToAdd);
        }
    }
}
