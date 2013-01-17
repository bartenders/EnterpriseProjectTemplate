// Type: IdeaBlade.Core.IHideObjectMembers
// Assembly: IdeaBlade.Core, Version=7.0.3.0, Culture=neutral, PublicKeyToken=287b5094865421c0
// Assembly location: C:\Projects\Playground\cocktail\Cocktail\Bin\Debug\IdeaBlade.Core.dll

using System;
using System.ComponentModel;

namespace EPT.Infrastructure.API
{
    /// <summary>
    /// Hides standard Object members from Intellisense
    ///             to make fluent interfaces easier to read.
    ///             May be implemented on any class.
    ///             Based on blog post by @kzu here:
    ///             http://bit.ly/ifluentinterface
    /// 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHideObjectMembers
    {
        /// <summary/>
        /// 
        /// <returns/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        /// <summary/>
        /// 
        /// <returns/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        /// <summary/>
        /// 
        /// <returns/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        /// <summary/>
        /// <param name="obj"/>
        /// <returns/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}
