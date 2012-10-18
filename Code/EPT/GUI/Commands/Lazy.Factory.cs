using System;
using System.Diagnostics.Contracts;

namespace EPT.GUI.Commands
{
    /// <summary>
    /// A factory class used to create instances of <see cref="Lazy{T}"/>.
    /// </summary>
    [Pure]
    public static class Lazy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy"/> class that uses a specified initialization function and a specified initialization 
        /// mode.
        /// </summary>
        /// <typeparam name="T">The type of element being lazily initialized.</typeparam>
        /// <param name="valueFactory">The <see cref="Func{T}"/> invoked to produce the lazily-initialized value when it is needed.</param>
        /// <param name="isThreadSafe">
        /// <c>true</c> if this instance should be usable by multiple threads concurrently; <c>false</c> if the instance will only be used by one 
        /// thread at a time.
        /// </param>
        /// <returns>A <see cref="Lazy"/> object.</returns>
        
        public static Lazy<T> Create<T>( Func<T> valueFactory, bool isThreadSafe = true)
        {
            Contract.Requires(valueFactory != null);
            Contract.Ensures(Contract.Result<Lazy<T>>() != null);
            Contract.Ensures(!Contract.Result<Lazy<T>>().IsValueCreated);

            return new Lazy<T>(valueFactory, isThreadSafe);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy"/> class that uses its default constructor and a specified initialization mode.
        /// </summary>
        /// <typeparam name="T">The type of element being lazily initialized.</typeparam>
        /// <param name="isThreadSafe">
        /// <c>true</c> if this instance should be usable by multiple threads concurrently; <c>false</c> if the instance will only be used by one 
        /// thread at a time.
        /// </param>
        /// <returns>A <see cref="Lazy"/> object.</returns>
        
        public static Lazy<T> Create<T>(bool isThreadSafe = true) where T : new()
        {
            Contract.Ensures(Contract.Result<Lazy<T>>() != null);
            Contract.Ensures(!Contract.Result<Lazy<T>>().IsValueCreated);

            return new Lazy<T>(isThreadSafe);
        }
    }
}