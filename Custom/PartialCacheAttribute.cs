using System;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AssessmentMVC.Custom
{
    public class PartialCacheAttribute : OutputCacheAttribute
    {
        public PartialCacheAttribute(string cacheProfileName)
        {
            OutputCacheProfile outputCacheProfile = ((OutputCacheSettingsSection)WebConfigurationManager.GetSection("system.web/caching/outputCacheSettings")).OutputCacheProfiles[cacheProfileName];
            base.Duration = outputCacheProfile.Duration;
            base.VaryByParam = outputCacheProfile.VaryByParam;
            base.VaryByCustom = outputCacheProfile.VaryByCustom;
        }
    }
}
