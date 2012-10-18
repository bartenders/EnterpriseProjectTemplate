using System;
using System.Linq;
using Caliburn.Micro;

namespace EPT.Infrastructure.Extensions
{
    public class CaliburnExtensions
    {
        /// <summary>
        /// Resolves the view type for a particular view model using caliburn
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <returns></returns>
        protected static object GetViewTypeForViewModel(Type viewModelType)
        {
            var viewTypeName = viewModelType.FullName;
            if (viewTypeName != null)
                viewTypeName = viewTypeName
                    .Substring(0, viewTypeName.IndexOf("`", StringComparison.Ordinal) < 0
                                      ? viewTypeName.Length
                                      : viewTypeName.IndexOf("`", StringComparison.Ordinal));

            var viewType = ViewLocator.TransformName(viewTypeName, null);
            object type = viewModelType.Assembly.GetType(viewType.FirstOrDefault());
            return type;
        }
    }
}