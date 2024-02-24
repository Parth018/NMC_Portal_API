using System;
using System.ComponentModel;
using System.Resources;

namespace ITMS.Business.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string resourceKey;

        private readonly ResourceManager resource;

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            resource = new ResourceManager(resourceType);
            this.resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                var displayName = resource.GetString(resourceKey);
                return string.IsNullOrEmpty(displayName) ? $"[[{resourceKey}]]" : displayName;
            }
        }
    }
}
