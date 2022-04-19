using AlphaWeb.Core.Common;
using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Configuration;
using System.Xml;

namespace AlphaWeb.Core.Configuration
{
    /// <summary>
    /// Represents startup AlphaWeb configuration parameters
    /// </summary>
    public class AlphaWebConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// Gets or sets a value indicating whether to display the full error in production environment.
        /// It's ignored (always enabled) in development environment
        /// </summary>
        public bool DisplayFullErrorStack { get; set; }

        /// <summary>
        /// Gets or sets a value of "Cache-Control" header value for static content
        /// </summary>
        public string StaticFilesCacheControl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we compress response
        /// </summary>
        public bool UseResponseCompression { get; set; }

        public DataSettings DataSettings { get; set; }
        /// <summary>
        /// Gets or sets connection string for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageConnectionString { get; set; }
        /// <summary>
        /// Gets or sets container name for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageContainerName { get; set; }
        /// <summary>
        /// Gets or sets end point for Azure BLOB storage
        /// </summary>
        public string AzureBlobStorageEndPoint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should use Redis server for caching (instead of default in-memory caching)
        /// </summary>
        public bool RedisCachingEnabled { get; set; }
        /// <summary>
        /// Gets or sets Redis connection string. Used when Redis caching is enabled
        /// </summary>
        public string RedisCachingConnectionString { get; set; }

        /// <summary>
        /// Gets or sets path to database with user agent strings
        /// </summary>
        public string UserAgentStringsPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we should support previous AlphaWeb versions (it can slightly improve performance)
        /// </summary>
        public bool SupportPreviousAlphaWebVersions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a owner can install sample data during installation
        /// </summary>
        public bool DisableSampleDataDuringInstallation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to use fast installation. 
        /// By default this setting should always be set to "False" (only for advanced users)
        /// </summary>
        public bool UseFastInstallationService { get; set; }
        public string PluginsIgnoredDuringInstallation { get; set; }
        public bool RunOnAzureWebApps { get; set; }

        public object Create(object parent, object configContext, XmlNode section)
        {
            AlphaWebConfig config = new AlphaWebConfig();

            XmlNode dataSettings = section.SelectSingleNode("DataSettings");

            config.DataSettings=new DataSettings
            {
                DataConnectionString = GetString(dataSettings, "DataConnectionString"),
                DataProvider = EnumParser.GetEnumValue<DataProviderType>(GetString(dataSettings, "DataProvider"))
            };
            XmlNode redisCachingNode = section.SelectSingleNode("RedisCaching");
            config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            XmlNode supportPreviousAlphaWebVersionsNode = section.SelectSingleNode("SupportPreviousNopcommerceVersions");
            config.SupportPreviousAlphaWebVersions = GetBool(supportPreviousAlphaWebVersionsNode, "Enabled");

            XmlNode webFarmsNode = section.SelectSingleNode("WebFarms");
            config.RunOnAzureWebApps = GetBool(webFarmsNode, "RunOnAzureWebApps");

            XmlNode azureBlobStorageNode = section.SelectSingleNode("AzureBlobStorage");
            config.AzureBlobStorageConnectionString = GetString(azureBlobStorageNode, "ConnectionString");
            config.AzureBlobStorageContainerName = GetString(azureBlobStorageNode, "ContainerName");
            config.AzureBlobStorageEndPoint = GetString(azureBlobStorageNode, "EndPoint");

            XmlNode installationNode = section.SelectSingleNode("Installation");
            config.DisableSampleDataDuringInstallation = GetBool(installationNode, "DisableSampleDataDuringInstallation");
            config.UseFastInstallationService = GetBool(installationNode, "UseFastInstallationService");
            config.PluginsIgnoredDuringInstallation = GetString(installationNode, "PluginsIgnoredDuringInstallation");

            return config;
        }
        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node == null || node.Attributes == null) return default(T);
            XmlAttribute attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            string attrVal = attr.Value;
            return converter(attrVal);
        }
    }
}