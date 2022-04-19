using DevExpress.XtraReports.Web.WebDocumentViewer;
using DevExpress.XtraReports.Web.WebDocumentViewer.Native.Services;
using System;

namespace Web.Framework
{
    public class CustomStoragesCleanerSettings : StoragesCleanerSettings
    {
        static StoragesCleanerSettings DefaultSettings = StoragesCleanerSettings.CreateDefault();
        public CustomStoragesCleanerSettings() : base(DefaultSettings.DueTime, DefaultSettings.Period, TimeSpan.FromHours(2), TimeSpan.FromHours(2), TimeSpan.FromHours(2))
        {
        }
    }

    public class CustomCacheCleanerSettings : CacheCleanerSettings
    {
        static CacheCleanerSettings DefaultSettings = CacheCleanerSettings.CreateDefault();
        public CustomCacheCleanerSettings() : base(DefaultSettings.DueTime, DefaultSettings.Period, TimeSpan.FromHours(2), TimeSpan.FromHours(2))
        {
        }
    }
}
