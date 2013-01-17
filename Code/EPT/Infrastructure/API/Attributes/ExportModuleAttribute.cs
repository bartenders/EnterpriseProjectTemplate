using System;
using System.ComponentModel.Composition;

namespace EPT.Infrastructure.API.Attributes
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExportModuleAttribute : ExportAttribute, IModuleMetadata
    {
        public int Order { get; private set; }
        public string Title { get; private set; }

        public ExportModuleAttribute(int order, string title)
            : base(typeof(IModule))
        {
            Order = order;
            Title = title;
        }
    }
}
